using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;

namespace ManagedAccessControlTranslator
{
    public class PoolSetAcceso
    {
        static PoolSetAcceso _instance;

        ManualResetEvent finalizarPoolSetAccesos = new ManualResetEvent(false);
        Queue<string> listaIDSerials = new Queue<string>();         // Lista de strings separados por coma, ID,SerialNum,TipoAcceso|ID,SerialNum,TipoAcceso|...
        ManualResetEvent continuarPoolSetAcceso = new ManualResetEvent(false);  

        static int _refCount = 0;                                   // Contador de referencias usadas por los translators. Si llega a cero se detiene el thread y se libera la referencia

        #region Singleton
        public static PoolSetAcceso GetInstance()
        {
            if (_instance == null)
                _instance = new PoolSetAcceso();
            return _instance;
        }

        PoolSetAcceso()
        {
            Start();                                                // Crea el thread 
        }
        #endregion

        public void addRefCount()
        {
            _refCount++;
            //Helpers.GetInstance().DoLog("Sumo refCount de poolSetAcceso =" + _refCount);
        }
        public void subRefCount()
        {
            _refCount--;
            Helpers.GetInstance().DoLog("Resto refCount de PoolSetAcceso =" + _refCount);
            Thread.Sleep(100);
            if (_refCount == 0)
            {
                Stop();                                 // Detiene el thread de verificacion
                Thread.Sleep(500);
                _instance = null;                       // Hace null la referencia para que un nuevo GetInstance lance todo de nuevo
                Helpers.GetInstance().DoLog("Instance de PoolSetAcceso es NULL");
            }
        }

        void Start()
        {
            Thread.Sleep(200);
            Helpers.GetInstance().DoLog("Start de PoolSetAcceso..");
            
            Thread t = new Thread(enviarListaSetAccesos);
            t.Start();

//            Task.Factory.StartNew(() => enviarListaSetAccesos());

        }

        void Stop()
        {
            Helpers.GetInstance().DoLog("PoolSetAcceso.Stop()");
            finalizarPoolSetAccesos.Set();
            continuarPoolSetAcceso.Set();

        }
        void enviarListaSetAccesos()
        {
            Helpers.GetInstance().DoLog("Comienza Task de envio de SET Accesos...");

            while (!finalizarPoolSetAccesos.WaitOne(2000))
            {
                Helpers.GetInstance().DoLog("Esperando datos de accesos de Mobiles");
                continuarPoolSetAcceso.WaitOne();                                // Espera que haya datos...
                continuarPoolSetAcceso.Reset();
                  Helpers.GetInstance().DoLog("Hay datos encolados");
                  if (!finalizarPoolSetAccesos.WaitOne(0))
                  {
                      try
                      {
                          if (listaIDSerials != null)
                          {
                              string IDSerialsToSend = "";
                              lock (listaIDSerials)
                              {
                                  while (listaIDSerials.Any())
                                  {
                                      string IDS = listaIDSerials.Dequeue();
                                      if (!String.IsNullOrEmpty(IDS))
                                          IDSerialsToSend += IDS.TrimEnd('|') + "|";
                                  }
                              }

                              if (!String.IsNullOrEmpty(IDSerialsToSend))
                              {
                                  string errDesc = "";
                                  int errCode = -1;
                                  bool done = false;
                                  while (!done && !finalizarPoolSetAccesos.WaitOne(0))
                                  {
                                      WebServiceAPI.GetInstance().AssignSerialNums(IDSerialsToSend, out errDesc, out errCode);
                                      if (errCode == (int)StatusCode.OK)
                                          done = true;
                                      else
                                      {
                                          Helpers.GetInstance().DoLog("Error al enviar serials de accesos: " + IDSerialsToSend + " " + errDesc);
                                          Thread.Sleep(1000);
                                      }
                                  }
                                  if (done)
                                    Helpers.GetInstance().DoLog("Enviados los IDSerials:" + IDSerialsToSend);

                                  PoolGetAcceso.GetInstance().ContinuarPoolGetAcceso();        // Como la llamada es bloqueante, si llega aca quiere decir que los envio todos.

                              }
                          }
                      }
                      catch (Exception ex)
                      {
                          Helpers.GetInstance().DoLog("EXCEPCION en enviarListaSetAccesos:" + ex.Message);
                      }
                  }
            }

            Helpers.GetInstance().DoLog("Finaliza Thread de actualizacion de SetAcceso");
        }

        public void addSetAcceso(string IDSerials)
        {
            lock (listaIDSerials)
            {
                listaIDSerials.Enqueue(IDSerials);
            }
        }

        public void ContinuarPool()
        {
            continuarPoolSetAcceso.Set();
        }

    }

       
}
