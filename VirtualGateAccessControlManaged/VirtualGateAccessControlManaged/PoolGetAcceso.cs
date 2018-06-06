using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;

namespace VirtualGateManaged
{
    public class PoolGetAcceso
    {
        static PoolGetAcceso _instance;

        ManualResetEvent finalizarPoolAccesos = new ManualResetEvent(false);
        ManualResetEvent continuarPool = new ManualResetEvent(true);                   // Para detener el pooling y evitar repeticion de altas en lenel. La primera vuelta sigue de largo
        Dictionary<string, Queue<Acceso>> accesosDevices = new Dictionary<string, Queue<Acceso>>();

        Dictionary<string, List<Acceso>> listaAccesosAnteriores = new Dictionary<string, List<Acceso>>();

        static int _refCount = 0;       // Contador de referencias usadas por los translators. Si llega a cero se detiene el thread y se libera la referencia

        #region Singleton
        public static PoolGetAcceso GetInstance()
        {
            if (_instance == null)
                _instance = new PoolGetAcceso();
            return _instance;
        }

        PoolGetAcceso()
        {
            Start();                    // Crea el thread de actualizacion de status de devices desde AlutelMobility
        }

        #endregion

        public void addRefCount()
        {
            _refCount++;
            Helpers.GetInstance().DoLog("Sumo refCount de poolGetAcceso =" + _refCount);
        }
        public void subRefCount()
        {
            _refCount--;
            if (_refCount == 0)
            {
                Stop();                                 // Detiene el thread de verificacion
                Thread.Sleep(200);
                _instance = null;                       // Hace null la referencia para que un nuevo GetInstance lance todo de nuevo
                Helpers.GetInstance().DoLog("Instance de PoolGetAcceso es NULL");
            }
            Helpers.GetInstance().DoLog("Resto refCount de PoolGetAcceso =" + _refCount);

        }

        void Start()
        {
            Thread.Sleep(200);
            Helpers.GetInstance().DoLog("Start de PoolGetAcceso..");

            //Task.Factory.StartNew(() => actualizarListaAccesos());
            Thread t = new Thread(actualizarListaAccesos);
            t.Start();

        }

        void Stop()
        {
            Helpers.GetInstance().DoLog("PoolGetAcceso.Stop()");
            finalizarPoolAccesos.Set();
            continuarPool.Set();
        }

        void actualizarListaAccesos()
        {
            Helpers.GetInstance().DoLog("Comienza Thread de actualizacion de Accesos...");

            while (!finalizarPoolAccesos.WaitOne(2500))
            {
                try
                {
                    //Helpers.GetInstance().DoLog("Esperando para hacer GetAccesoGeneral");

                    continuarPool.WaitOne();                       // No sigue hasta que le avisen desde el PoolSetAcceso que se confirme.
                    continuarPool.Reset();
                    if (!finalizarPoolAccesos.WaitOne(0))           // Continuar si no se seteo la salida del Task.
                    {
                      //  Helpers.GetInstance().DoLog("Va a hacer getAccesosGeneral...");
                        
                        List<string> panelesActuales = new List<string>();
                        lock (VirtualGateManagedTranslator.ListaPanelNames)
                        {
                            foreach (string panel in VirtualGateManagedTranslator.ListaPanelNames)
                                panelesActuales.Add(panel);
                        }

                        Dictionary<string, List<Acceso>> listaAccesos = WebServiceAPI.GetInstance().GetAccesosGeneral(panelesActuales,Helpers.GetInstance().MainOrgID.ToString());
                        //Helpers.GetInstance().DoLog("Volvio de getAccesosGeneral");
                        
                        bool hizoAdd = false;

                        if (listaAccesos != null)
                        {
                            if (listaAccesos.Count > 0)
                            {
                                lock (accesosDevices)
                                {
                                    int cantAdded = 0;
                                    foreach (KeyValuePair<string, List<Acceso>> par in listaAccesos)
                                    {
                                        cantAdded += addAcceso(par.Key, par.Value);
                                       
                                    }
                                    if (cantAdded > 0)
                                        hizoAdd = true;
                                    else
                                        hizoAdd = false;
                                }
                            }
                        }

                        if (!hizoAdd)
                        {
                            //Helpers.GetInstance().DoLog("No Hizo ADD: ContinuarPool directamente....");
                            Thread.Sleep(2000);              // Pausa antes de continuar
                            ContinuarPool();                // Si no hizo ningun add continuar el pooling directamente.
                        }
                        if (listaAccesos != null)
                            listaAccesosAnteriores = listaAccesos;

                    }
                }
                catch (Exception ex)
                {
                    Helpers.GetInstance().DoLog("EXCEPCION en actualizarListaAccesos: " + ex.Message);
                    Helpers.GetInstance().DoLog("ContinuarPool directamente....");
                    Thread.Sleep(2000);              // Pausa antes de continuar
                    ContinuarPool();        // Para destrancar en caso de OutOfMemory...
                }

            }

            Helpers.GetInstance().DoLog("Finaliza Thread de actualizacion de GetAcceso");
        }

        public int addAcceso(string deviceName, List<Acceso> accesos)
        {
            int cantAdded = 0;
            try
            {
                if (!VirtualGateManagedTranslator.ListaPanelNames.Contains(deviceName))
                {
                    Helpers.GetInstance().DoLog("Accesos del PanelName=" + deviceName + " descartados por no estar contraladas por el LnlCommServer de esta OC");
                    return 0;
                }

                lock (accesosDevices)
                {
                    if (!accesosDevices.ContainsKey(deviceName))
                        accesosDevices.Add(deviceName, new Queue<Acceso>());

                    foreach (Acceso acceso in accesos)
                    {
                        if (existeEnAnteriores(deviceName, acceso)) continue;        // Lo descarta si ya lo encolo antes
                        if (existeEnActuales(deviceName, acceso)) continue;          // Lo descarta si ya se encolo ahora.

                        accesosDevices[deviceName].Enqueue(acceso);
                        Helpers.GetInstance().DoLog("Encolados accesos de " + deviceName + ": Nombre=" + acceso.Nombre + " Apellido=" + acceso.Apellido + " Tarjeta=" + acceso.Tarjeta + " Hora=" + acceso.Hora + " TipoAcceso=" + acceso.tipoAcceso);
                        cantAdded++;

                    }
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en addAcceso: " + ex.Message);
            }
            return cantAdded;
        }

        private bool existeEnActuales(string deviceName, Acceso acceso)
        {
            bool existe = false;
            if (accesosDevices.ContainsKey(deviceName))
            {
                foreach (Acceso a in accesosDevices[deviceName])
                {
                    if ((a.Tarjeta == acceso.Tarjeta) && (a.Hora == acceso.Hora) && (a.tipoAcceso == acceso.tipoAcceso))
                    {
                        Helpers.GetInstance().DoLog("DESCARTADO POR YA ENCOLADO acceso con tarjeta=" + a.Tarjeta + " hora=" + a.Hora + " tipoAcceso=" + a.tipoAcceso);
                        existe = true;
                        break;
                    }
                }

            }
            return existe;

        }
        private bool existeEnAnteriores(string deviceName, Acceso acceso)
        {
            bool existe = false;
            if (listaAccesosAnteriores.ContainsKey(deviceName))
            {
                foreach (Acceso a in listaAccesosAnteriores[deviceName])
                {
                    if ((a.Tarjeta == acceso.Tarjeta) && (a.Hora == acceso.Hora) && (a.tipoAcceso == acceso.tipoAcceso))
                    {
                        Helpers.GetInstance().DoLog("DESCARTADO acceso con tarjeta=" + a.Tarjeta + " hora=" + a.Hora + " TipoAcceso=" + a.tipoAcceso);
                        existe = true;
                        break;
                    }
                }

            }
            return existe;
        }



        public string GetAccesos(string deviceName)
        {
            string res = "";
            try
            {
                lock (accesosDevices)
                {
                    if (accesosDevices.ContainsKey(deviceName))
                    {
                        while (accesosDevices[deviceName].Any())
                        {
                            Acceso acceso = accesosDevices[deviceName].Dequeue();
                            //if ((acceso.tipoAcceso == TiposAcceso.INVALIDO) || (acceso.tipoAcceso == TiposAcceso.EINVALIDO) || (acceso.tipoAcceso == TiposAcceso.SINVALIDO))
                            //    res += acceso.AccessType + "," + acceso.Tarjeta + "," + acceso.PanelID + "," + acceso.ReaderID + "," + acceso.Hora + "," + acceso.idAcceso.ToString() + ",V|";
                            //else
                            //    res += acceso.AccessType + "," + acceso.Tarjeta + "," + acceso.PanelID + "," + acceso.ReaderID + "," + acceso.Hora + "," + acceso.idAcceso.ToString() + ",A|";

                            string identificadorTipoAcceso = "";

                            //if (acceso.tipoAcceso == TiposAcceso.INVALIDO)
                            //    identificadorTipoAcceso = ",V|";
                            //else
                            //{
                            //    if (acceso.Cruce)
                            //        identificadorTipoAcceso = ",C|";
                            //    else
                            //        identificadorTipoAcceso = ",A|";
                            //}

                            if (acceso.Cruce)
                                identificadorTipoAcceso = ",C|";
                            else
                            {
                                if (acceso.esVisita)
                                    identificadorTipoAcceso = ",V|";
                                else
                                    identificadorTipoAcceso = ",A|";
                            }


                            res += acceso.AccessType + "," + acceso.Tarjeta + "," + acceso.PanelID + "," + acceso.ReaderID + "," + acceso.Hora + "," + acceso.idAcceso.ToString() + identificadorTipoAcceso;
                        }

                        return res;
                    }
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en GetAccesos. Devicename=" + deviceName + " error=" + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Devuelve false si aun hay accesos encolados para algun device.
        /// </summary>
        /// <returns></returns>
        public bool isEmpty()
        {
            bool res = true;
            lock (accesosDevices)
            {
                foreach (KeyValuePair<string, Queue<Acceso>> par in accesosDevices)
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

        public void ContinuarPool()
        {
            continuarPool.Set();
        }


    }
}
