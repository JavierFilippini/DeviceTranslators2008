using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ManagedAccessControlTranslator;
//using System.Threading.Tasks;

namespace ManagedAccessControlTranslator
{
    public class PoolGetAlarm
    {
        static PoolGetAlarm _instance;

        ManualResetEvent finalizarPoolAlarmas = new ManualResetEvent(false);
        ManualResetEvent continuarPoolGet = new ManualResetEvent(true);                   // Para detener el pooling y evitar repeticion de altas en lenel. La primera vuelta sigue de largo
        Dictionary<int, Queue<AlarmaAlutel>> alarmasDevices = new Dictionary<int, Queue<AlarmaAlutel>>();
        Dictionary<int, List<AlarmaAlutel>> listaAlarmasAnteriores = new  Dictionary<int, List<AlarmaAlutel>>();

        static int _refCount = 0;       // Contador de referencias usadas por los translators. Si llega a cero se detiene el thread y se libera la referencia

        #region Singleton
        public static PoolGetAlarm GetInstance()
        {
            if (_instance == null)
                _instance = new PoolGetAlarm();
            return _instance;
        }

        PoolGetAlarm()
        {
            Start();                    // Crea el thread de actualizacion de status de devices desde AlutelMobility
        }

        #endregion

        public void addRefCount()
        {
            _refCount++;
            //Helpers.GetInstance().DoLog("Sumo refCount de poolGetAlarm =" + _refCount);
        }
        public void subRefCount()
        {
            _refCount--;
            Helpers.GetInstance().DoLog("Resto refCount de PoolGetAlarm =" + _refCount);
            Thread.Sleep(100);
            if (_refCount == 0)
            {
                Stop();                                 // Detiene el thread de verificacion
                Thread.Sleep(500);
                _instance = null;                       // Hace null la referencia para que un nuevo GetInstance lance todo de nuevo
                Helpers.GetInstance().DoLog("Instance de PoolGetAlarm es NULL");
            }
           

        }

        void Start()
        {
            Helpers.GetInstance().DoLog("Start de PoolGetAlarm..");
            Thread.Sleep(250);

            Thread t = new Thread(actualizarListaAlarmas);
            t.Start();
            //Task.Factory.StartNew(() => actualizarListaAlarmas());
        }


        void Stop()
        {
            Helpers.GetInstance().DoLog("PoolGetAlarm.Stop()");
            finalizarPoolAlarmas.Set();
            continuarPoolGet.Set();

        }
        void actualizarListaAlarmas()
        {
            Helpers.GetInstance().DoLog("Comienza Task de actualizacion de Alarmas...");

            while (!finalizarPoolAlarmas.WaitOne(5000))
            {
                try
                {

                    //Helpers.GetInstance().DoLog("Esperando para hacer GetAlarmasGeneral()");
                    continuarPoolGet.WaitOne();                       // No sigue hasta que le avisen desde el PoolSetAlarma que se confirme.
                    continuarPoolGet.Reset();

                   // Helpers.GetInstance().DoLog("Continua ...");

                    if (!finalizarPoolAlarmas.WaitOne(0))           // Continuar si no se seteo la salida del Task.
                    {
                      //  Helpers.GetInstance().DoLog("Va a hacer GetAlarmasGeneral...");

                        List<int> panelesActuales = new List<int>();
                        lock (ManagedAccessControl.ListaPanelIDs)
                        {
                            foreach (int panelID in ManagedAccessControl.ListaPanelIDs)
                                panelesActuales.Add(panelID);
                        }

                        Dictionary<int, List<AlarmaAlutel>> listaAlarmas = WebServiceAPI.GetInstance().GetAlarmasGeneral(panelesActuales);
                      //  Helpers.GetInstance().DoLog("Volvio de GetAlarmasGeneral");

                        bool hizoAdd = false;
                        if (listaAlarmas != null)
                        {
                            if (listaAlarmas.Count > 0)
                            {
                                lock (alarmasDevices)
                                {
                                    int cantAdded = 0;
                                    foreach (KeyValuePair<int, List<AlarmaAlutel>> par in listaAlarmas)
                                    {
                                        cantAdded += addAlarma(par.Key, par.Value);
                                    }
                                    if (cantAdded > 0)
                                    {
                                        listaAlarmasAnteriores = listaAlarmas;          // Para chequear la proxima vuelta.
                                        hizoAdd = true;
                                    }
                                    else
                                    {
                                        listaAlarmasAnteriores.Clear();
                                        hizoAdd = false;
                                    }
                                }
                            }
                        }
                        if (!hizoAdd)
                            ContinuarPoolGet();        // Si no hizo ningun add continuar el pooling directamente
                    }
                }
                catch (Exception ex)
                {
                    Helpers.GetInstance().DoLog("EXCEPCION en actualizarListaAlarmas:" + ex.Message);
                }
            }

            Helpers.GetInstance().DoLog("Finaliza Thread de actualizacion de Alarmas.");
        }

        public int addAlarma(int panelID, List<AlarmaAlutel> alarmas)
        {
            int cantAdded = 0;

            //if (!ManagedAccessControl.ListaPanelIDs.Contains(panelID))
            //{
            //    Helpers.GetInstance().DoLog("Alarmas del PanelID=" + panelID + " descartadas por no estar contraladas por el LnlCommServer de esta PC");
            //    return 0;
            //}

            lock (alarmasDevices)
            {
                if (!alarmasDevices.ContainsKey(panelID))
                    alarmasDevices.Add(panelID, new Queue<AlarmaAlutel>());


                foreach (AlarmaAlutel alarma in alarmas)
                {
                    bool add = true;        // No repetir el encolado si ya fue encolado. 
                    if (listaAlarmasAnteriores.ContainsKey(panelID))
                    {
                        // add = false;
                        foreach (AlarmaAlutel al in listaAlarmasAnteriores[panelID])
                        {
                            if ((al.DeviceID == alarma.DeviceID) && (al.EventID == alarma.EventID) && (al.EventType == alarma.EventType) && (al.Hora == alarma.Hora))
                                add = false;
                        }
                    }
                    if (add)
                    {
                        alarmasDevices[panelID].Enqueue(alarma);
                        cantAdded++;
                    }
                }
            }
            return cantAdded;
        }

        public string GetAlarm(int panelID)
        {
            lock (alarmasDevices)
            {
                if (alarmasDevices.ContainsKey(panelID))
                {
                    if (alarmasDevices[panelID].Any())
                    {
                        AlarmaAlutel al = alarmasDevices[panelID].Dequeue();

                        string evType = ((int)al.EventType).ToString();
                        string evID = ((int)al.EventID).ToString();

                        string encAlarm = "ALARM:" + evType + "|" + evID + "|" + al.Texto + "|" + al.Hora.ToString("yyyy-MM-dd HH:mm:ss") + "|" + al.ID.ToString() + "|" + al.tipoAlarma;
                        return encAlarm;
                    }
                }
                
                return String.Empty;
            }
        }


        /// <summary>
        /// Devuelve false si aun hay accesos encolados para algun device.
        /// </summary>
        /// <returns></returns>
        public bool isEmpty()
        {
            bool res = true;
            lock (alarmasDevices)
            {
                foreach (KeyValuePair<int, Queue<AlarmaAlutel>> par in alarmasDevices)
                {
                    if (par.Value.Any())
                    {
                        res = false;
                        break;
                    }
                }
            }

            return res;
        }

        public void ContinuarPoolGet()
        {
            continuarPoolGet.Set();
        }
    }
}
