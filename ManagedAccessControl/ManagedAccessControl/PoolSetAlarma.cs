using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;

namespace ManagedAccessControlTranslator
{
    public class PoolSetAlarma
    {
        static PoolSetAlarma _instance;

        ManualResetEvent finalizarPoolSetAlarmas = new ManualResetEvent(false);
        Queue<AlarmaIDSerial> listaAlarmasIDSerials = new Queue<AlarmaIDSerial>();

        ManualResetEvent continuarPoolSet = new ManualResetEvent(false);  

        static int _refCount = 0;       // Contador de referencias usadas por los translators. Si llega a cero se detiene el thread y se libera la referencia

        #region Singleton
        public static PoolSetAlarma GetInstance()
        {
            if (_instance == null)
                _instance = new PoolSetAlarma();
            return _instance;
        }

        PoolSetAlarma()
        {
            Start();                    // Crea el thread 
        }
        #endregion

        public void addRefCount()
        {
            _refCount++;
            //Helpers.GetInstance().DoLog("Sumo refCount de PoolSetAlarma =" + _refCount);
        }
        public void subRefCount()
        {
            _refCount--;
            Helpers.GetInstance().DoLog("Resto refCount de PoolSetAlarma =" + _refCount);
            Thread.Sleep(100);
            if (_refCount == 0)
            {
                Stop();                                 // Detiene el thread de verificacion
                Thread.Sleep(500);
                _instance = null;                       // Hace null la referencia para que un nuevo GetInstance lance todo de nuevo
                Helpers.GetInstance().DoLog("Instance de PoolSetAlarma es NULL");
            }
        }

        void Start()
        {
            Thread.Sleep(200);
            Helpers.GetInstance().DoLog("Start de PoolSetAlarma..");

            Thread t = new Thread(enviarListaSetAlarmas);
            t.Start();

//            Task.Factory.StartNew(() => enviarListaSetAlarmas());

        }


        void Stop()
        {
            Helpers.GetInstance().DoLog("PoolSetAlarma.Stop()");
            finalizarPoolSetAlarmas.Set();
            continuarPoolSet.Set();

        }
        void enviarListaSetAlarmas()
        {
            Helpers.GetInstance().DoLog("Comienza Task de envio de SET Alarmas...");

            while (!finalizarPoolSetAlarmas.WaitOne(2000))
            {

                Helpers.GetInstance().DoLog("Esperando datos de Alarmas...");
                continuarPoolSet.WaitOne();                                                    // Espera que haya datos...
                continuarPoolSet.Reset();
                Helpers.GetInstance().DoLog("Hay datos de Alarmas encolados");
                
                if (!finalizarPoolSetAlarmas.WaitOne(0))
                {
                    try
                    {

                        if (listaAlarmasIDSerials != null)
                        {
                            List<AlarmaIDSerial> listaToSend = new List<AlarmaIDSerial>();
                            lock (listaAlarmasIDSerials)
                            {
                                while (listaAlarmasIDSerials.Any())
                                {
                                    AlarmaIDSerial IDS = listaAlarmasIDSerials.Dequeue();

                                    listaToSend.Add(IDS);
                                }
                            }

                            if (listaToSend.Count > 0)
                            {
                                //string errDesc = "";
                                //int errCode = -1;
                                //WebServiceAPI.GetInstance().AssignSerialnumsAlarmas(listaToSend, out errDesc, out errCode);

                                string errDesc = "";
                                int errCode = -1;
                                bool done = false;
                                while (!done && !finalizarPoolSetAlarmas.WaitOne(0))
                                {
                                    WebServiceAPI.GetInstance().AssignSerialnumsAlarmas(listaToSend, out errDesc, out errCode);
                                    if (errCode == (int)StatusCode.OK)
                                        done = true;
                                    else
                                    {
                                        Helpers.GetInstance().DoLog("Error al enviar serials de Alarmas: " + listaToSend.ToString() + " " + errDesc);
                                        Thread.Sleep(1000);
                                    }
                                }

                                Helpers.GetInstance().DoLog("Hecha la asignacion de serialnums en Alarmas.");
                            
                                PoolGetAlarm.GetInstance().ContinuarPoolGet();        // si ya llegaron todos los envios.    

                                //Helpers.GetInstance().DoLog("Hizo ContinuarPool de GetAlarm()");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Helpers.GetInstance().DoLog("EXCEPCION en enviarListaSetAlarmas:" + ex.Message);
                    }
                }
            }

            Helpers.GetInstance().DoLog("Finaliza Thread de actualizacion de SetAlarmas");
        }

        public void addSetAlarma(string alarmID, string tipoAlarma, string serialNum)
        {
            lock (listaAlarmasIDSerials)
            {
                AlarmaIDSerial al = new AlarmaIDSerial();
                al.ID = alarmID;
                al.tipoAlarma = tipoAlarma;
                al.SerialNum = serialNum;

                listaAlarmasIDSerials.Enqueue(al);
            }
        }
        public void ContinuarPoolSet()
        {
            continuarPoolSet.Set();
        }

    }


}