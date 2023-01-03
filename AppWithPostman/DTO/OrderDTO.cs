using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.DTO
{
    public class OrderDTO
    {
        public int IdOrder { get; set; }
        public string Anteprima { get; set; }
        public decimal Total_Iva { get; set; }
        public decimal Total_Order { get; set; }
        public string Sorgente_Fronte { get; set; }
        public string Carrier { get; set; }
        public string Sorgente_Retro { get; set; }
        public Account_Name Account_Name { get; set; }
        public decimal Sub_Total { get; set; }
        public string Subject { get; set; }
        public string IdZoho { get; set; }
        public Nullable<bool> IsDeletedInZoho { get; set; }
        public decimal? PrezzoCalcolatoNetto { get; set; }
        public int? qta { get; set; }

        
        public DateTime? DataIns { get; set; }
        
        public DateTime? DataCambioStato { get; set; }
        public DateTime? DataPrevProduzione { get; set; }
        
        public DateTime? DataPrevConsegna { get; set; }

        public decimal? Sconto { get; set; }
        public int Tipologia_di_consegna { get; set; }
        public int Status { get; set; }

        public int IdOrdineInt { get; set; } 
        public string Tipologia_di_consegnastr
        {
            get
            {
                switch (Tipologia_di_consegna)
                {
                    case 0:
                        return "CN";

                    case 1:
                        return "RC";
                    case 2:
                        return "Prova";
                    default:
                        return "Prova";
                }
            }
        }
        public string Statusstr
        {
            get
            {
                switch (Status)
                {
                    case 5:
                        return "PROVA";
                    case 7:
                        return "Registrato";
                    case 10:
                        return "Preinserito";
                    case 20:
                        return "Registrato";
                    case 21:
                        return "In Sospeso";
                    case 22:
                        return "In Coda di Stampa";
                    case 31:
                        return "In Attesa di finitura";
                    case 32:
                        return "PROVA";
                    case 40:
                        return "In Imballaggio";
                    case 50:
                        return "Pronto per il ritiro";
                    case 51:
                        return "Uscito da magazzino";
                    case 60:
                        return "In consegna";
                    case 70:
                        return "Consegnato";
                    case 80:
                        return "Acconto";
                    case 81:
                        return "Pagato";
                    default:
                        return "PROVA";
                }
            }
        }
        public string Note { get; set; }
    }
}
