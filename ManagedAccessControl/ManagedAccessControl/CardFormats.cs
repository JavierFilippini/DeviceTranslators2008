using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagedAccessControlTranslator
{
    public class CardFormats
    {

        int idCardFormat = -1;
        string nombre = "";
        int facilityCode = -1;
        int numeroTotalBits = -1;

        int facilityCodeStart = -1;
        int facilityCodeEnd = -1;

        int cardNumberStart = -1;
        int cardNumberEnd = -1;

        int idOrganizacion = 0;

        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string FacilityCode { get; set; }
        public string Desfasaje { get; set; }
        public short ICStart { get; set; }
        public short ICEnd { get; set; }

        public CardFormats(int pidCardFormat, string pnombre, int pfacilityCode, int pnumeroTotalBits, int pfacilityCodeStart, int pfacilityCodeEnd, int pcardNumberStart, int pcardNumberEnd, int pidOrganizacion)
        {
            idCardFormat = pidCardFormat;
            nombre = pnombre;
            facilityCode = pfacilityCode;
            numeroTotalBits = pnumeroTotalBits;
            facilityCodeStart = pfacilityCodeStart;
            facilityCodeEnd = pfacilityCodeEnd;
            cardNumberStart = pcardNumberStart;
            cardNumberEnd = pcardNumberEnd;
            idOrganizacion = pidOrganizacion;
        }
        public CardFormats()
        {
        }

        public int IdCardFormat
        {
            get
            {
                return idCardFormat;
            }
            set
            {
                idCardFormat = value;
            }
        }
        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }
        public int FacilityCode
        {
            get
            {
                return facilityCode;
            }
            set
            {
                facilityCode = value;
            }
        }
        public int NumeroTotalBits
        {
            get
            {
                return numeroTotalBits;
            }
            set
            {
                numeroTotalBits = value;
            }
        }
        public int FacilityCodeStart
        {
            get
            {
                return facilityCodeStart;
            }
            set
            {
                facilityCodeStart = value;
            }
        }
        public int FacilityCodeEnd
        {
            get
            {
                return facilityCodeEnd;
            }
            set
            {
                facilityCodeEnd = value;
            }
        }
        public int CardNumberStart
        {
            get
            {
                return cardNumberStart;
            }
            set
            {
                cardNumberStart = value;
            }
        }
        public int CardNumberEnd
        {
            get
            {
                return cardNumberEnd;
            }
            set
            {
                cardNumberEnd = value;
            }
        }
        public int IdOrganizacion
        {
            get
            {
                return idOrganizacion;
            }
            set
            {
                idOrganizacion = value;
            }

        }

    }
}
