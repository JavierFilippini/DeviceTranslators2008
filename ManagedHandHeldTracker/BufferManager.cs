using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;

namespace ManagedHandHeldTracker
{
    class BufferManager
    {
        Regex Multiple_Pos_Data = new Regex(@"GPS:(.*);HH:(.*)");  // Cada dato es: ID,Lat,Long,Speed,DateTime, ... 5 por cada bloque

        int punteroBuffer = 0;

        private const int MINIMUM_BUFFER_TO_DIVIDE = 200;
        private const int BUFFERDIVIDER = 8;
        private Dictionary<string, Zone.GeoCoord> buffer1 = new Dictionary<string,Zone.GeoCoord>();
        private Dictionary<string, Zone.GeoCoord> buffer2 = new Dictionary<string, Zone.GeoCoord>();
        private bool isBufferLoaded = false;

    #region singleton
        static BufferManager _instance;
        public static BufferManager GetInstance()
        {
            if (_instance == null) _instance = new BufferManager();
            return _instance;
        }

        BufferManager()
        {
        }

        public void resetBuffer()
        {
            punteroBuffer = 0;
            buffer1 = new Dictionary<string, Zone.GeoCoord>();
            buffer2 = new Dictionary<string, Zone.GeoCoord>();

        }
        public Dictionary<string, Zone.GeoCoord> ObtenerSiguienteBuffer()
        {
            try
            {
                //Tools.GetInstance().DoLog("1");

                if (buffer1.Count < MINIMUM_BUFFER_TO_DIVIDE)
                {
                    buffer1 = ObtenerSiguienteBufferPosiciones();
                    if (buffer1.Count < MINIMUM_BUFFER_TO_DIVIDE)       // Pregunta otra vez para el caso de comienzo en que buffer1 es vacio y por si se agregan devices, para cambiar la estrategia on the fly
                        return buffer1;
                }


                //if (buffer1.Count < BUFFERDIVIDER)
                //{
                //  //  Tools.GetInstance().DoLog("2");
                //    buffer1 = ObtenerSiguienteBufferPosiciones();
                //   // Tools.GetInstance().DoLog("3. buffer1.count=" + buffer1.Count);
                //    if (buffer1.Count < BUFFERDIVIDER)
                //        return buffer1;
                //    Tools.GetInstance().DoLog("4");
                //}

                punteroBuffer++;
               // Tools.GetInstance().DoLog("punterobuffer=" + punteroBuffer);

                if (punteroBuffer == (1 + (int)(BUFFERDIVIDER / 2)))
                {
                    isBufferLoaded = false;
                    //Tools.GetInstance().DoLog("Va a cargar buffer2");
                    //Task.Factory.StartNew(() => CargarSiguienteBufferPosiciones());
                    Thread t = new Thread(CargarSiguienteBufferPosiciones);
                    t.Start();

                }
                if (punteroBuffer > BUFFERDIVIDER)
                {
                    if (isBufferLoaded)
                    {
                        lock (buffer2)
                        {
                            buffer1 = CopyBufferPuntos(buffer2, 0, buffer2.Count);
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }
                        isBufferLoaded = false;
                        punteroBuffer = 1;
                    }
                    else
                    {
                        Tools.GetInstance().DoLog("buffer 2 no esta listo aun. punterobuffer=" + punteroBuffer);
                        return null;
                    }

                }
                

                if (punteroBuffer == BUFFERDIVIDER)
                    return CopyBufferPuntos(buffer1, (punteroBuffer - 1) * (int)(buffer1.Count / BUFFERDIVIDER), (buffer1.Count - (punteroBuffer - 1) * (int)(buffer1.Count / BUFFERDIVIDER)));
                else
                    return CopyBufferPuntos(buffer1, (punteroBuffer - 1) * (int)(buffer1.Count / BUFFERDIVIDER), (buffer1.Count / BUFFERDIVIDER));
                
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("excepcion en ObtenerSiguienteBuffer: " + ex.Message);

            }
            return null;
        }

        private Dictionary<string, Zone.GeoCoord> CopyBufferPuntos(Dictionary<string, Zone.GeoCoord> buffer, int start, int cantidad)
        {

            //Tools.GetInstance().DoLog("Va a CopyBufferPuntos: start="+ start + " cant=" + cantidad);

            Dictionary<string, Zone.GeoCoord> res = new Dictionary<string, Zone.GeoCoord>();

            try
            {
                if (buffer != null)
                {
                    var arrayKeys = buffer.Keys.ToArray();

                    for (int i = start; i < start + cantidad; i++)
                    {
                        res.Add(arrayKeys[i], buffer[arrayKeys[i]]);
                    }
                }
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en CopyBufferPuntos: " + ex.Message);
            }
            return res;
        }


        /// <summary>
        /// Carga completamente la siguiente definicion de los puntos GPS
        /// </summary>
        /// <returns></returns>
        private void CargarSiguienteBufferPosiciones()
        {
            Dictionary<string, Zone.GeoCoord> res = ObtenerSiguienteBufferPosiciones();

            lock (buffer2)
            {
                buffer2 = res;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            isBufferLoaded = true;
            //Tools.GetInstance().DoLog("Cargo buffer2: " + buffer2.Count);

        }

        private Dictionary<string, Zone.GeoCoord> ObtenerSiguienteBufferPosiciones()
        {
            Dictionary<string, Zone.GeoCoord> res = new Dictionary<string, Zone.GeoCoord>();

            long PanelIDSelected = long.Parse(frmLiveTrackingVG.DEVICEID);

            try
            {
                string errDesc = "";
                int errCode = -1;
                string datos = WebServiceAPI.GetInstance().GetMultiplePositionsFromZone(PanelIDSelected.ToString(), frmLiveTrackingVG.ORGID.ToString(), out errDesc, out errCode);
                //Helpers.GetInstance().DoLog("datos=" + datos);
                Match matchRespuesta = Multiple_Pos_Data.Match(datos);

                if (matchRespuesta.Success)
                {
                    string[] HHPositions = getMatchData(matchRespuesta, 2).Split(',');              // Cada dato es: PANELID,Lat,Long,Speed,DateTime, ... 5 por cada bloque

                    for (int i = 0; i < HHPositions.Length - 1; i = i + 5)
                    {
                        //long ID = Convert.ToInt64(HHPositions[i]); // Es el PANELID del HH
                        string nombrePanel = HHPositions[i];
                        string lat = HHPositions[i + 1];
                        string lng = HHPositions[i + 2];
                        string speed = HHPositions[i + 3];
                        string dateTime = HHPositions[i + 4];

                        Zone.GeoCoord nuevoGPS = new Zone.GeoCoord(lat, lng);
                        res.Add(nombrePanel, nuevoGPS);
                    }
                }
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en ObtenerSiguienteBufferPosiciones(): " + ex.Message);
            }
            //Tools.GetInstance().DoLog("ObtenerSiguienteBufferPosiciones devolvio:" + res.Count );
            return res;
        }

        /// <summary>
        /// Extrae un campo de una expresion regular reconocida
        /// </summary>
        public string getMatchData(Match resultMatch, int index)
        {
            return resultMatch.Groups[index].Value;
        }

    #endregion








    }
}
