using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagedAccessControlTranslator
{
    public enum TiposAcceso             // Es el tipo de evento que viene desde el HH
    {
        NoDefinido,
        Entrada,
        Salida,
        EINVALIDO,
        SINVALIDO,
        INVALIDO,
        Alarma,
        Diferido,
        Bloqueo                         // Viene desde el HH cuando esta en modo BLOQUEO

    }


    public class Acceso
    {
        public int idAcceso { get; set; }               //Creado por santiago en proceso de migracion
        public string HHID { get; set; }
        public string ReaderName { get; set; }
        public string Tarjeta { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Hora { get; set; }

        public TiposAcceso tipoAcceso { get; set; }     // Si fue permitido, denegado, etc.
        public string AccessType { get; set; }
        public int idEmpleado { get; set; }
        public int PanelID { get; set; }
        public int ReaderID { get; set; }
        public int orgID { get; set; }
        public int idImagenAcceso { get; set; }         // id en la tabla ImagenesAccesos
        public int idImagenEmpleado { get; set; }       // id en la tabla ImagenesEmpleados
        public int idImagenGPS { get; set; }            // id en la tabla ImagenesGPS
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NumeroDoc { get; set; }
        public string Empresa { get; set; }
        public string Company { get; set; }
        public int idAlarma { get; set; }
        public DateTime UltimaActualizacion { get; set; }
        public string SerialNumber { get; set; }
        public int Credito { get; set; }
        public bool Cruce { get; set; }                 // True implica que es un acceso generado por el cruce de un panel a una Zona virtual. Su info esta en la tabla CrucesVZone
        public bool esVisita { get; set; }              // True implica que el accesos es DENIED y por lo tanto su info esta en la tabla VisitaPorlevantar. 
                                                        // False implica que esta en AccesosPorLevantar
        public Acceso()
        {

        }
    }
}
