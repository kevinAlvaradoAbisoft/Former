using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppWithPostman.DTO
{
    public class Contact
    {
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        //public string Email { get; set; }
        //public string Description { get; set; }
        //public string Mailing_Zip { get; set; }
        //public string Mailing_State { get; set; }
        //public string Mailing_Street { get; set; }
        //public object followers { get; set; }
        //public object phonedqwqw { get; set; }
        //public object Last_Activity_Time { get; set; }
        //public string First_Name { get; set; }
        //public string Full_Name { get; set; }
        //public object Record_Image { get; set; }
        //public string Phone { get; set; }
        //public string Mailing_Country { get; set; }
        //public bool Email_Opt_Out { get; set; }
        //public DateTime Modified_Time { get; set; }
        //public object Mailing_City { get; set; }
        //public DateTime Created_Time { get; set; }
        //public string Title { get; set; }
        //public string Mobile { get; set; }
        //public string Home_Phone { get; set; }
        //public string Last_Name { get; set; }
        //public string Secondary_Email { get; set; }

        public int Id_Cliente { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        
        public string Full_Name { get; set; }

        public string Tipologia { get; set; }
        public string Partita_Iva { get; set; }
        public string Codice_Fiscale { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string E_mail_amministrazione { get; set; }
        public string PEC { get; set; }
        public string SDI { get; set; }
        public AccountNameCompanie Account_Name { get; set; }
    }
    public class AccountNameCompanie
    {
        public string name { get; set; }
        public string id { get; set; }
    }

   
}
