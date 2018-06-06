using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagedHandHeldTracker
{
    public class Device
    {
        public int ID { get; set; }
        public int Mode { get; set; }
        public bool Active { get; set; }
        public DateTime LastModification { get; set; }
        public int UltimoIntegrador { get; set; }
        public int LNLPanelID = 0;                  // El LNLPanelID por defecto es 0
        public string HHID;
        public int idMarca;
        public int idModelo;
        public int organizacion;
        public string softwareVersion;
        public string deviceTypename;
        public int deviceType;

        public Device(string v_HHID, int pmarca, int pmodelo, int PanelID, int idOrganizacion)
        {
            HHID = v_HHID;
            LNLPanelID = PanelID;
            idMarca = pmarca;
            idModelo = pmodelo;
            organizacion = idOrganizacion;

        }

        public Device(Device Original)
        {
            HHID = Original.HHID;
            LNLPanelID = Original.LNLPanelID;
            idMarca = Original.idMarca;
            idModelo = Original.idModelo;
            organizacion = Original.organizacion;
            //softwareVersion = Original.softwareVersion;
        }

        public Device()
        {

        }
    }
}
