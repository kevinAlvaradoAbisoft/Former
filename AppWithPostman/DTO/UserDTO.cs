using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.DTO
{
    public class UserDTO
    {
        public int IdUser { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Pec { get; set; }
        public string Sdi { get; set; }
        public string Codice_Fiscale { get; set; }
        public string Partita_Iva { get; set; }
        public string IdZoho { get; set; }
        public Nullable<bool> IsDeletedInZoho { get; set; }
        public string IdZohoAziende { get; set; }

    }
}
