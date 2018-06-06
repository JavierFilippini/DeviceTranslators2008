using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagedHandHeldTracker
{
    public class TarjetaMaster
    {
        public string Numero { get; set; }
        public TiposAcceso Tipo { get; set; }
        public string VirtualZone { get; set; }
        public int PanelID { get; set; }
    }
}
