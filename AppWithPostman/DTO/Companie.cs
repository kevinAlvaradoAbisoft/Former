using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.DTO
{
    public class Companie
    {
        public DatumCompanie[] data { get; set; }
    }


    public class DatumCompanie
    {
        //public Owner Owner { get; set; }

        //[JsonProperty("$currency_symbol")]
        //public string currency_symbol { get; set; }

        //[JsonProperty("$field_states")]
        //public object field_states { get; set; }
        //public object Account_Type { get; set; }
        //public object SIC_Code { get; set; }
        //public object Last_Activity_Time { get; set; }
        //public object Industry { get; set; }
        //public string Tipologia { get; set; }
        //public object Account_Site { get; set; }

        [JsonProperty("$state")]
        public string state { get; set; }

        //[JsonProperty("$process_flow")]
        //public bool process_flow { get; set; }
        //public object Billing_Country { get; set; }
        //public string id { get; set; }

        [JsonProperty("$approved")]
        public bool approved { get; set; }

        //[JsonProperty("$approval")]
        //public Approval approval { get; set; }
        public string Enrich_Status__s { get; set; }
        //public object Billing_Street { get; set; }
        //public DateTime Created_Time { get; set; }

        [JsonProperty("$editable")]
        public bool editable { get; set; }
        //public object Billing_Code { get; set; }
        //public object Shipping_City { get; set; }
        //public object Shipping_Country { get; set; }
        //public object Shipping_Code { get; set; }
        //public object Billing_City { get; set; }
        //public CreatedBy Created_By { get; set; }
        //public object Annual_Revenue { get; set; }
        //public object Shipping_Street { get; set; }
        //public object Ownership { get; set; }
        //public object Description { get; set; }
        //public object Rating { get; set; }
        //public object Shipping_State { get; set; }

        //[JsonProperty("$review_process")]
        //public ReviewProcess review_process { get; set; }
        //public object Website { get; set; }
        //public object Employees { get; set; }
        //public object Record_Image { get; set; }
        //public ModifiedBy Modified_By { get; set; }

        //[JsonProperty("$review")]
        //public object review { get; set; }
        public string Phone { get; set; }
        public string Account_Name { get; set; }
        //public string Account_Number { get; set; }
        //public object Ticker_Symbol { get; set; }
        //public DateTime Modified_Time { get; set; }

        //[JsonProperty("$orchestration")]
        //public bool orchestration { get; set; }
        //public object Parent_Account { get; set; }
        public string s { get; set; }
        //public Layout Layout { get; set; }

        //[JsonProperty("$in_merge")]
        //public bool in_merge { get; set; }
        //public object Billing_State { get; set; }
        //public List<object> Tag { get; set; }
        //public object Fax { get; set; }

        [JsonProperty("$approval_state")]
        public string approval_state { get; set; }

        //[JsonProperty("$pathfinder")]
        //public object pathfinder { get; set; }
        //public object Last_Enriched_Time__s { get; set; }

        [JsonIgnore]
        public int Id_Cliente { get; set; }

        

    }






}
