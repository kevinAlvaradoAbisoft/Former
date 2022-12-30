//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppWithPostman
{
    using System;
    using System.Collections.Generic;
    
    public partial class Utenti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utenti()
        {
            this.Ordini = new HashSet<Ordini>();
        }
    
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordini> Ordini { get; set; }
    }
}
