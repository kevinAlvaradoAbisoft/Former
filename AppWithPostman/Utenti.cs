//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppWithPostman
{
    using System;
    using System.Collections.Generic;
    
    public partial class Utenti
    {
        public int IdUt { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Nullable<int> IdRubricaInt { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public string RagSoc { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Indirizzo { get; set; }
        public Nullable<int> IdProvincia { get; set; }
        public Nullable<int> IdComune { get; set; }
        public string Citta { get; set; }
        public string Provincia { get; set; }
        public string Cap { get; set; }
        public string Piva { get; set; }
        public string CodFisc { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Cellulare { get; set; }
        public int TipoRub { get; set; }
        public Nullable<int> TipoWeb { get; set; }
        public Nullable<int> IdCorriereDef { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public string LastIp { get; set; }
        public string SitoWeb { get; set; }
        public Nullable<int> IdPagamento { get; set; }
        public Nullable<System.DateTime> DataIns { get; set; }
        public Nullable<int> IdTipoAttivita { get; set; }
        public Nullable<int> DownloadEsplicito { get; set; }
        public Nullable<int> NoMail { get; set; }
        public string CodiceSDI { get; set; }
        public string Pec { get; set; }
        public Nullable<int> UpdateFromUser { get; set; }
        public Nullable<int> NoCheckDatiFisc { get; set; }
        public Nullable<int> IdNazione { get; set; }
        public string PrefissoPIva { get; set; }
        public Nullable<int> DisattivaAccessoSito { get; set; }
        public string IdZoho { get; set; }
        public string ZohoId { get; set; }
        public Nullable<bool> IsDeletedInZoho { get; set; }
        public string IdZohoAziende { get; set; }
    }
}
