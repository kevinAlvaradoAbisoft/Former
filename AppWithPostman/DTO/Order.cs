using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.DTO
{
    public class Order
    {
        /*
        ID ordine IdOrdine
        ID cliente IdUt
        Prodotto (Tagliato dal Codice) 
        Nome lavoro
        Quantità qta
        Prezzo 
        Corriere
        Data di inserimento
        Data di Consegna
        Stato dell'Ordine
        Link anteprima
        Link Sorgente fronte
        Link Sorgente retro
        */
        public List<DatumOrder> data { get; set; }
        //public Info info { get; set; }
    }
    public class AccountName
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Approval
    {
        public bool @delegate { get; set; }
        public bool approve { get; set; }
        public bool reject { get; set; }
        public bool resubmit { get; set; }
    }

    public class ContactName
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class CreatedBy
    {
        public string name { get; set; }
        public string id { get; set; }
        public string email { get; set; }
    }

    public class DatumOrder
    {
        //public Owner? Owner { get; set; }

        [JsonProperty("$currency_symbol")]
        public string currency_symbol { get; set; }
        public int? Quantit { get; set; }

        [JsonProperty("$field_states")]
        public object field_states { get; set; }
        public int Tax { get; set; }
        public string Anteprima { get; set; }
        public object Sorgente_Fronte { get; set; }

        [JsonProperty("$state")]
        public string state { get; set; }

        [JsonProperty("$converted")]
        public bool converted { get; set; }

        [JsonProperty("$process_flow")]
        public bool process_flow { get; set; }
        public object Deal_Name { get; set; }
        public object Billing_Country { get; set; }
        //public string id { get; set; }
        public string Carrier { get; set; }

        [JsonProperty("$approved")]
        public bool approved { get; set; }
        public QuoteName Quote_Name { get; set; }
        public string Status { get; set; }
        public int Grand_Total { get; set; }

        [JsonProperty("$approval")]
        //public Approval approval { get; set; }
        public object Billing_Street { get; set; }
        public int Adjustment { get; set; }
        public DateTime? Created_Time { get; set; }

        [JsonProperty("$editable")]
        public bool editable { get; set; }
        public object Billing_Code { get; set; }
        public List<ProductDetail> Product_Details { get; set; }
        public object Sorgente_Retro { get; set; }
        public int? Id_Ordine { get; set; }
        public object Excise_Duty { get; set; }
        public object Shipping_City { get; set; }
        public object Shipping_Country { get; set; }
        public object Shipping_Code { get; set; }
        public object Billing_City { get; set; }
        public object Purchase_Order { get; set; }
        public CreatedBy Created_By { get; set; }
        public object Shipping_Street { get; set; }
        public object Description { get; set; }
        public int Discount { get; set; }
        public object Shipping_State { get; set; }

        [JsonProperty("$review_process")]
        public ReviewProcess review_process { get; set; }
        public ModifiedBy Modified_By { get; set; }

        [JsonProperty("$review")]
        public object review { get; set; }
        public AccountName Account_Name { get; set; }
        public string Tipologia_di_consegna { get; set; }
        public object Sales_Commission { get; set; }
        public DateTime Modified_Time { get; set; }
        public object Due_Date { get; set; }
        public string Data_di_consegna { get; set; }
        public object Terms_and_Conditions { get; set; }
        public int Sub_Total { get; set; }
        public string Data_di_inserimento { get; set; }
        public string Subject { get; set; }

        [JsonProperty("$orchestration")]
        public bool orchestration { get; set; }
        public ContactName Contact_Name { get; set; }
        public Layout Layout { get; set; }

        [JsonProperty("$in_merge")]
        public bool in_merge { get; set; }
        public string SO_Number { get; set; }
        public object Billing_State { get; set; }

        [JsonProperty("$line_tax")]
        public List<LineTax> line_tax { get; set; }
        public List<object> Tag { get; set; }

        [JsonProperty("$approval_state")]
        public string approval_state { get; set; }

        [JsonProperty("$pathfinder")]
        public object pathfinder { get; set; }
        public object Pending { get; set; }
    }

    public class Info
    {
        public int per_page { get; set; }
        public int count { get; set; }
        public int page { get; set; }
        public string sort_by { get; set; }
        public string sort_order { get; set; }
        public bool more_records { get; set; }
    }

    public class Layout
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class LineTax
    {
        public int percentage { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public int value { get; set; }
    }

    public class ModifiedBy
    {
        public string name { get; set; }
        public string id { get; set; }
        public string email { get; set; }
    }

    public class Owner
    {
        public string name { get; set; }
        public string id { get; set; }
        public string email { get; set; }
    }

    public class Product
    {
        public string Product_Code { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public class ProductDetail
    {
        public Product product { get; set; }
        public int quantity { get; set; }
        public int Discount { get; set; }
        public int total_after_discount { get; set; }
        public int net_total { get; set; }
        public object book { get; set; }
        public int? Tax { get; set; }
        public int list_price { get; set; }
        public int? unit_price { get; set; }
        public int quantity_in_stock { get; set; }
        public int total { get; set; }
        public int? id { get; set; }
        public object product_description { get; set; }
        public List<object> line_tax { get; set; }
    }

    public class QuoteName
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class ReviewProcess
    {
        public bool approve { get; set; }
        public bool reject { get; set; }
        public bool resubmit { get; set; }
    }
}