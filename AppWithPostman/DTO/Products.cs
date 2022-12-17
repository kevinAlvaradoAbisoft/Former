using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.DTO
{

    public class DatumProduct
    {
        public string currency_symbol { get; set; }
        public int Quantit { get; set; }
        public object field_states { get; set; }
        public int Tax { get; set; }
        public object Anteprima { get; set; }
        public object Sorgente_Fronte { get; set; }
        public string state { get; set; }
        public bool converted { get; set; }
        public bool process_flow { get; set; }
        public object Deal_Name { get; set; }
        public object Billing_Country { get; set; }
        public string Carrier { get; set; }
        public bool approved { get; set; }
        public object Quote_Name { get; set; }
        public string Status { get; set; }
        public int Grand_Total { get; set; }
        public object Billing_Street { get; set; }
        public int Adjustment { get; set; }
        public bool editable { get; set; }
        public object Billing_Code { get; set; }
        public ProductDetails[] Product_Details { get; set; }
        public object Sorgente_Retro { get; set; }
        public int Id_Ordine { get; set; }
        public object Excise_Duty { get; set; }
        public object Shipping_City { get; set; }
        public object Shipping_Country { get; set; }
        public object Shipping_Code { get; set; }
        public object Billing_City { get; set; }
        public object Purchase_Order { get; set; }
        public object Shipping_Street { get; set; }
        public object Description { get; set; }
        public int Discount { get; set; }
        public object Shipping_State { get; set; }
        public object review { get; set; }
        public Account_Name Account_Name { get; set; }
        public string Tipologia_di_consegna { get; set; }
        public object Sales_Commission { get; set; }
        public object Due_Date { get; set; }
        public string Data_di_consegna { get; set; }
        public object Terms_and_Conditions { get; set; }
        public int Sub_Total { get; set; }
        public string Data_di_inserimento { get; set; }
        public string Subject { get; set; }
        public bool orchestration { get; set; }
        public object Contact_Name { get; set; }
    }

    public class Account_Name
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class ProductDetails
    {
        public ProductZoho product { get; set; }
        public int quantity { get; set; }
        public int Discount { get; set; }
        public int total_after_discount { get; set; }
        public int net_total { get; set; }
        public object book { get; set; }
        public object Tax { get; set; }
        public int list_price { get; set; }
        public object unit_price { get; set; }
        public int quantity_in_stock { get; set; }
        public int total { get; set; }
        public object product_description { get; set; }
        public object[] line_tax { get; set; }
    }

    public class ProductZoho
    {
        public string Product_Code { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }
}
