using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagedHandHeldTracker
{
    public class Tarjeta
    {
        public long id { get; set; }
        public string tarjeta { get; set; }
        public int idEmpleado { get; set; }
        public int estado { get; set; }
        public string accessLevels { get; set; }
        public DateTime ultimaActualizacion { get; set; }
        public int OrgID { get; set; }

        public DateTime activationDate { get; set; }
        public DateTime deactivationDate { get; set; }
        public bool isVisitor { get; set; }
        public int UltimoIntegrador { get; set; }
        public string PIN { get; set; }
        public int idTipoTarjeta { get; set; }
        public int lnlbadgekey { get; set; }

        // NOTA: el -1 en idEmpleado es para identificar un empleado con tarjeta no definida
        public Tarjeta(int pidOrg, int pidTarjeta, string pnumerodetarjeta, int pidEmpleado, int pestado, string v_accessLevels, DateTime v_ultAct, DateTime v_actDate, DateTime v_deactDate, bool v_isVisit, string v_PIN, int tipoTarjeta, int v_lnlbadgekey)
        {
            tarjeta = pnumerodetarjeta;
            OrgID = pidOrg;
            id = pidTarjeta;
            idEmpleado = pidEmpleado;
            estado = pestado;
            accessLevels = v_accessLevels;
            ultimaActualizacion = v_ultAct;
            activationDate = v_actDate;
            deactivationDate = v_deactDate;
            isVisitor = v_isVisit;
            PIN = v_PIN;
            idTipoTarjeta = tipoTarjeta;
            lnlbadgekey = v_lnlbadgekey;
        }
        public Tarjeta()
        {
            idEmpleado = -1;
            accessLevels = string.Empty;
            PIN = string.Empty;
            lnlbadgekey = -1;
        }

        public Tarjeta(Tarjeta original)            // Crea un copia de la tarjeta en el constructor.
        {
            tarjeta = original.tarjeta;
            OrgID = original.OrgID;
            id = original.id;
            idEmpleado = original.idEmpleado;
            estado = original.estado;
            accessLevels = original.accessLevels;
            ultimaActualizacion = original.ultimaActualizacion;
            activationDate = original.activationDate;
            deactivationDate = original.deactivationDate;
            isVisitor = original.isVisitor;
            PIN = original.PIN;
            idTipoTarjeta = original.idTipoTarjeta;
            lnlbadgekey = original.lnlbadgekey;
        }
    }
}
