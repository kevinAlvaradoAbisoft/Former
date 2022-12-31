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
    }
}
