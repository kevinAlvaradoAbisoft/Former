using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.DTO
{
    public class OrderDTO
    {
        public int IdOrder { get; set; }
        public object Anteprima { get; set; }
        public decimal Total_Iva { get; set; }
        public decimal Total_Order { get; set; }
        public object Sorgente_Fronte { get; set; }
        public string Carrier { get; set; }
        public object Sorgente_Retro { get; set; }
        public Account_Name Account_Name { get; set; }
        public decimal Sub_Total { get; set; }
        public string Subject { get; set; }
        public string IdZoho { get; set; }
        public Nullable<bool> IsDeletedInZoho { get; set; }
    }
}
