using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ManagedAccessControlTranslator;
//using System.Threading.Tasks;

namespace ManagedAccessControlTranslator
{
    public class PoolGetConnStatus
    {
        static PoolGetConnStatus _instance;


        ManualResetEvent finalizarPoolStatus = new ManualResetEvent(false);
        Dictionary<int, bool> statusDevices = new Dictionary<int, bool>();
        static int _refCount = 0;       // Contador de referencias usadas por los translators. Si llega a cero se detiene el thread y se libera la referencia

        #region Singleton
        public static PoolGetConnStatus GetInstance()
        {
            if (_instance == null)
                _instance = new PoolGetConnStatus();
            return _instance;
        }

        PoolGetConnStatus()
        {
            Start();                                    // Crea el thread de actualizacion de status de devices desde AlutelMobility
        }
        
        #endregion

        public void addRefCount()
        {
            _refCount++;
            //Helpers.GetInstance().DoLog("Sumo refCount de PoolGetConnStatus =" + _refCount);
        }
        public  void subRefCount()
        {
            _refCount--;
            Helpers.GetInstance().DoLog("Resto refCount de PoolGetConnStatus =" + _refCount);
            Thread.Sleep(100);
            if (_refCount == 0)
            {
                Stop();                                 // Detiene el thread de verificacion
                Thread.Sleep(500);
                _instance = null;                       // Hace null la referencia para que un nuevo GetInstance lance todo de nuevo
                Helpers.GetInstance().DoLog("Instance de PoolGetConnStatus es NULL");
            }

          

        }

        void Start()
        {
            Thread.Sleep(150);
            Helpers.GetInstance().DoLog("Start de PoolGetConnStatus");

            Thread t = new Thread(actualizarConnStatus);
            t.Start();
//            Task.Factory.StartNew(() => actualizarConnStatus());
        }

        void Stop()
        {
            Helpers.GetInstance().DoLog("PoolGetConnStatus.Stop()");
            finalizarPoolStatus.Set();
            
        }
        void actualizarConnStatus()
        {
            Helpers.GetInstance().DoLog("Comienza Task de actualizacion de ConnStatus...");
          
            while (!finalizarPoolStatus.WaitOne(5000))
            {
                try
                {
                    Dictionary<int, bool> listaStatus = WebServiceAPI.GetInstance().GetConnStatusMobileGeneral();

                    if (listaStatus != null)
                    {
                        lock (statusDevices)
                        {
                            foreach (KeyValuePair<int, bool> par in listaStatus)
                            {
                                setConnStatus(par.Key, par.Value);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helpers.GetInstance().DoLog("EXCEPCION en actualizarConnStatus:" + ex.Message);
                }
            }

            Helpers.GetInstance().DoLog("Finaliza Thread de actualizacion de ConnStatus.");
        }

        public void setConnStatus(int panelID, bool status)
        {
            lock (statusDevices)
            {
                if (!statusDevices.ContainsKey(panelID))
                    statusDevices.Add(panelID, status);
                else
                    statusDevices[panelID] = status;
            }
        }

        public bool getConnStatusMobile(int panelID)
        {
            lock (statusDevices)
            {
                if (statusDevices.ContainsKey(panelID))
                    return statusDevices[panelID];
                else
                    return false;
            }
        }



    }
}
