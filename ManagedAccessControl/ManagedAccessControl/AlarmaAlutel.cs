using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagedAccessControlTranslator
{
    public enum OnGuardEventType        // Tipos de Eventos OnGuard
    {
        L_EVENTTYPE_GENERIC = 30,
        L_EVENTTYPE_SYSTEM = 4,
        L_EVENTTYPE_EMERGENCY = 2
    }
    public enum OnGuardEventID        	// ID de eventos OnGuard
    {
        L_GENERIC_GENERIC_EVENT = 0,                            // El device envia un texto de alarma al server.
        L_SYSTEM_POWERFAILURE = 20,                             // El device se quedo sin bateria: se va a apagar
        L_SYSTEM_READER_LOW_BATTERY = 544,                      // El device tiene baja bateria
        L_SYSTEM_READER_LOW_BATTERY_RESTORED = 545,             // El device se recupero de una baja bateria
        L_EMERGENCY_DURESS = 18,                                // Emergencia
        L_SYSTEM_COMMUNICATION_TROUBLE_RESTORE = 247             // Se recupero del modo bloqueo.

    }
   


    public class AlarmaAlutel
    {
        public int ID { get; set; }
        public string DeviceName { get; set; }
        public int DeviceID { get; set; }
        public OnGuardEventType EventType { get; set; }
        public OnGuardEventID EventID { get; set; }
        public DateTime Hora { get; set; }
        public string Texto { get; set; }
        public int Organizacion { get; set; }
        public int idExteno { get; set; }
        public char tipoAlarma { get; set; }
        public DateTime UltimaActualizacion { get; set; }
        public string SerialNum { get; set; }

        public AlarmaAlutel(string v_DeviceName, OnGuardEventType v_EventType, OnGuardEventID v_EventID, DateTime v_Hora, string v_Texto, int v_Organizacion, int v_idExterno, char tipoAlarma)
        {

            this.DeviceName = v_DeviceName;

            this.EventType = v_EventType;
            this.EventID = v_EventID;
            this.Hora = v_Hora;
            this.Texto = v_Texto;
            this.Organizacion = v_Organizacion;
            this.ID = 0;                            // No asignado en el constructor.
            this.idExteno = v_idExterno;
            this.tipoAlarma = tipoAlarma;           // Tabla fuente de la alarma: A: ALARMASALUTEL, C: ACCESOS, V: VISITA

        }

        public AlarmaAlutel()
        {

        }

    }
}
