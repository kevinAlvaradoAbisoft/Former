using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;
using System.Runtime.InteropServices;
using AppWithPostman.DTO;
using AppWithPostman.Repository;
using System.Threading;
using System.Data.Common;
using Newtonsoft.Json.Linq;

namespace AppWithPostman
{
    class Program
    {
        const string ACCOUNTS_URL = "https://accounts.zoho.eu/oauth/v2/token";
        static void Main(string[] args)
        {
            string _code = ConfigurationSettings.AppSettings["code"];
            string _client_id = ConfigurationSettings.AppSettings["client_id"];
            string _secret_id = ConfigurationSettings.AppSettings["secret_id"];
            string _grant_type = ConfigurationSettings.AppSettings["grant_type"];
            string _redirect_url = ConfigurationSettings.AppSettings["redirect_url"];
            var client = new RestClient(ACCOUNTS_URL);

            var Token_Refresh = "";
            var Token_Work = "";
            var token = TokenRepository.GetToken();
            if (token == null)
            {

                var requestToken = new RestRequest(ACCOUNTS_URL, Method.Post);
                requestToken.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                requestToken.AddHeader("Cookie", "_zcsr_tmp=dce17c2b-03ed-4a5a-8490-ee8c4589b5df; b266a5bf57=57c7a14afabcac9a0b9dfc64b3542b70; iamcsr=dce17c2b-03ed-4a5a-8490-ee8c4589b5df");
                requestToken.AddParameter("code", _code);
                requestToken.AddParameter("redirect_url", _redirect_url);
                requestToken.AddParameter("client_id", _client_id);
                requestToken.AddParameter("client_secret", _secret_id);
                requestToken.AddParameter("grant_type", "authorization_code");
                var response = client.Execute(requestToken);

                Console.WriteLine(response.Content);
                GenerarToken accesToken = JsonConvert.DeserializeObject<GenerarToken>(response.Content);

                string urlEncoded = ACCOUNTS_URL + "?Content-Type=application/x-www-form-urlencoded";
                var requestEncoded = new RestRequest(urlEncoded, Method.Post);
                requestEncoded.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                requestEncoded.AddHeader("Cookie", "_zcsr_tmp=dce17c2b-03ed-4a5a-8490-ee8c4589b5df; b266a5bf57=57c7a14afabcac9a0b9dfc64b3542b70; iamcsr=dce17c2b-03ed-4a5a-8490-ee8c4589b5df");
                requestEncoded.AddParameter("refresh_token", accesToken.refresh_token);
                requestEncoded.AddParameter("client_id", _client_id);
                requestEncoded.AddParameter("client_secret", _secret_id);
                requestEncoded.AddParameter("grant_type", "refresh_token");
                var responseEncoded = client.Execute(requestEncoded);

                GenerarToken generationToken = JsonConvert.DeserializeObject<GenerarToken>(responseEncoded.Content);
                Token_Refresh = accesToken.refresh_token;
                Token_Work = generationToken.access_token;
                TokenRepository.AddToken(Token_Refresh, Token_Work);
            }
            else
            {
                var timetoken = DateTime.Now - token.LastUpdate;
                if ((timetoken.Value.Hours * 60 + timetoken.Value.Minutes) < 59)
                {
                    Token_Refresh = token.Token_Refresh;
                    Token_Work = token.Token_Work;
                }
                else
                {
                    string urlEncoded = ACCOUNTS_URL + "?Content-Type=application/x-www-form-urlencoded";
                    var requestEncoded = new RestRequest(urlEncoded, Method.Post);
                    requestEncoded.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    requestEncoded.AddHeader("Cookie", "_zcsr_tmp=dce17c2b-03ed-4a5a-8490-ee8c4589b5df; b266a5bf57=57c7a14afabcac9a0b9dfc64b3542b70; iamcsr=dce17c2b-03ed-4a5a-8490-ee8c4589b5df");
                    requestEncoded.AddParameter("refresh_token", token.Token_Refresh);
                    requestEncoded.AddParameter("client_id", _client_id);
                    requestEncoded.AddParameter("client_secret", _secret_id);
                    requestEncoded.AddParameter("grant_type", "refresh_token");
                    var responseEncoded = client.Execute(requestEncoded);

                    GenerarToken generationToken = JsonConvert.DeserializeObject<GenerarToken>(responseEncoded.Content);
                    Token_Refresh = token.Token_Refresh;
                    Token_Work = generationToken.access_token;
                    TokenRepository.UpdateToken(Token_Refresh, Token_Work);
                }
            }

            // add and update Azienda

            List<Utenti> utentiCompanie = CompanieRepository.GetUtentiCompanie();
            List<DatumCompanie> _datiCompanie = new List<DatumCompanie>();
            int numeroContattiCompanie = 1;
            foreach (var _utente in utentiCompanie)
            {
                if (numeroContattiCompanie <= 10)
                {
                    _datiCompanie.Add(
                            new DatumCompanie()
                            {
                                state = "save",
                                approved = true,
                                Enrich_Status__s = "Disponibile",
                                editable = true,

                                Phone = _utente.Tel,
                                Account_Name = _utente.Nome + " " + _utente.Cognome,
                                s = "Disponibile",
                                approval_state = "approved",
                                Id_Cliente = _utente.IdUt
                            }
                        );
                    numeroContattiCompanie++;
                }
                else
                {
                    AddUpdateCompani(Token_Work, _datiCompanie);
                    numeroContattiCompanie = 1;
                    _datiCompanie.Clear();

                    _datiCompanie.Add(
                        new DatumCompanie()
                        {
                            state = "save",
                            approved = true,
                            Enrich_Status__s = "Disponibile",
                            editable = true,

                            Phone = _utente.Tel,
                            Account_Name = _utente.Nome + " " + _utente.Cognome,
                            s = "Disponibile",
                            approval_state = "approved",
                            Id_Cliente = _utente.IdUt
                        }
                    );
                }

            }
            if (_datiCompanie.Count > 0)
            {
                AddUpdateCompani(Token_Work, _datiCompanie);
            }



            //add and update Contact process
            List<Utenti> utenti = UtentiRepository.GetUtenti();
            List<Datum> _dati = new List<Datum>();
            int numeroContatti = 1;
            foreach (var _utente in utenti)
            {
                if (numeroContatti <= 10)
                {
                    _dati.Add(
                            new Datum()
                            {

                                Full_Name = _utente.Nome + " " + _utente.Cognome, //aumente ya que me dio error que es campo necesrio
                                Email = _utente.Email,
                                First_Name = _utente.Nome,
                                Last_Name = _utente.Cognome,
                                Phone = _utente.Tel,
                                PEC = _utente.Pec,
                                SDI = _utente.CodiceSDI,
                                Codice_Fiscale = string.IsNullOrEmpty(_utente.CodFisc) ? "0" : _utente.CodFisc,
                                Id_Cliente = _utente.IdUt,
                                Partita_Iva = string.IsNullOrEmpty(_utente.Piva) ? "0" : _utente.Piva,
                                Mobile = _utente.Cellulare,
                                Account_Name = new AccountNameCompanie
                                {
                                    name = _utente.Nome + " " + _utente.Cognome,
                                    id = _utente.IdZohoAziende
                                }
                            }
                        );
                    numeroContatti++;
                }
                else
                {
                    AddUpdateContact(Token_Work, _dati);
                    numeroContatti = 1;
                    _dati.Clear();

                    _dati.Add(
                        new Datum()
                        {

                            Full_Name = _utente.Nome + " " + _utente.Cognome, //aumente ya que me dio error que es campo necesrio
                            Email = _utente.Email,
                            First_Name = _utente.Nome,
                            Last_Name = _utente.Cognome,
                            Phone = _utente.Tel,
                            PEC = _utente.Pec,
                            SDI = _utente.CodiceSDI,
                            Codice_Fiscale = string.IsNullOrEmpty(_utente.CodFisc) ? "0" : _utente.CodFisc,
                            Id_Cliente = _utente.IdUt,
                            Partita_Iva = string.IsNullOrEmpty(_utente.Piva) ? "0" : _utente.Piva,
                            Mobile = _utente.Cellulare,
                            Account_Name = new AccountNameCompanie
                            {
                                name = _utente.Nome + " " + _utente.Cognome,
                                id = _utente.IdZohoAziende
                            }
                        }
                    );
                }

            }
            if (_dati.Count > 0)
            {
                AddUpdateContact(Token_Work, _dati);
            }


            //delete process
            /*List<string> arrayItem1 = new List<string>();
            List<Utenti> utenti2 = UtentiRepository.GetUtentiDelete();
            if (utenti2 != null)
            {
                int _count = 1;
                var lista = "";
                foreach (var _utentedel in utenti2)
                {

                    if (_count <= 100)
                    {
                        arrayItem1.Add(_utentedel.ZohoId);
                        lista = lista + _utentedel.ZohoId + ",";
                    }
                    else
                    {
                        lista = "";
                        _count = 1;
                        arrayItem1.Clear();

                        lista = lista + _utentedel.ZohoId + ",";
                        arrayItem1.Add(_utentedel.ZohoId);
                        DeleteZohoContact(lista, arrayItem1, Token_Work);

                    }
                    _count++;
                }
                if (!string.IsNullOrEmpty(lista))
                {       
                    DeleteZohoContact(lista, arrayItem1, Token_Work);
                }
            }*/

            //delete companie

            List<string> arrayItem1 = new List<string>();
            List<Utenti> utenti2 = CompanieRepository.GetCompanieDelete();
            if (utenti2 != null)
            {
                int _count = 1;
                var lista = "";
                foreach (var _utentedel in utenti2)
                {

                    if (_count <= 100)
                    {
                        arrayItem1.Add(_utentedel.IdZohoAziende);
                        lista = lista + _utentedel.IdZohoAziende + ",";
                    }
                    else
                    {
                        lista = "";
                        _count = 1;
                        arrayItem1.Clear();

                        lista = lista + _utentedel.IdZohoAziende + ",";
                        arrayItem1.Add(_utentedel.IdZohoAziende);
                        DeleteZohoCompanie(lista, arrayItem1, Token_Work);

                    }
                    _count++;
                }
                if (!string.IsNullOrEmpty(lista))
                {
                    DeleteZohoCompanie(lista, arrayItem1, Token_Work);
                }
            }

            //order
            List<Ordini> ordini = OrdiniRepository.GetOrders();
            List<DatumOrder> _datiOrder = new List<DatumOrder>();
            List<ProductDetail> productList = new List<ProductDetail>();

            int numberOrder = 1;

            var data = ordini[0];

            foreach (var _order in ordini)
            {
                productList.Add(new ProductDetail
                {
                    product = new Product
                    {
                        Product_Code = "Offset",
                        name = "Offset",
                        id= "517570000000607003"
                    },
                    quantity = 5,
                    Discount=0,
                    total_after_discount=0,
                    net_total= 0,
                    book= null,
                    Tax = null,
                    list_price = 0,
                    unit_price= null,
                    quantity_in_stock=-1,
                    total= (int)_order.TotaleOrdine,
                    product_description=null,
                });

                _datiOrder.Add(
                        new DatumOrder()
                        {
                            currency_symbol = "€",
                            Quantit= 5,
                            field_states = null,
                            Tax= (int)_order.TotaleIva,
                            Anteprima = _order.Anteprima,
                            Sorgente_Fronte = _order.SorgenteFronte,
                            state = "save",
                            converted= false,
                            process_flow= false,
                            Deal_Name= null,
                            Billing_Country=null,
                            Carrier= _order.T_Corriere.Descrizione,
                            approved = true,
                            Quote_Name= null,
                            Status= "Registrato",
                            Grand_Total= (int)_order.TotaleOrdine,
                                
                            Billing_Street= null,
                            Adjustment= 77,
                            editable = true,
                            Billing_Code= null,
                            Product_Details = productList,
                            Sorgente_Retro= _order.SorgenteRetro,
                            //Id_Ordine = 14589,
                            Excise_Duty= null,
                            Shipping_City= null,
                            Shipping_Country = null,
                            Shipping_Code = null,
                            Billing_City= null, 
                            Purchase_Order= null,

                            Shipping_Street = null,
                            Description = null,
                            Discount = 0,
                            Shipping_State= null,

                            review = null ,
                            Account_Name = new AccountName
                            {
                                name = _order.Utenti.Nome,
                                id = _order.Utenti.IdZohoAziende
                            },
                            Tipologia_di_consegna = "Ritiro da Cliente",
                            Sales_Commission= null,

                            Due_Date = null,
                            Data_di_consegna = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd"),
                            Terms_and_Conditions= null,
                            Sub_Total= (int)_order.TotaleNetto,
                            Data_di_inserimento= DateTime.Now.ToString("yyyy-MM-dd"),
                            Subject = _order.T_listinobase.Nome,
                            orchestration = false,
                            Contact_Name= null

                        }
                    );
                AddUpdateOrder(Token_Work, _datiOrder);
                numberOrder = 1;
                _datiOrder.Clear();
                productList.Clear();
            }

        }
        private static void AddUpdateContact(string Token_Work, List<Datum> _dati)
        {
            var client = new RestClient("https://accounts.zoho.eu/oauth/v2/Saten");

            string urlLeads = "https://www.zohoapis.eu/crm/v3/Contacts/upsert";
            var requesturlLeads = new RestRequest(urlLeads, Method.Post);
            requesturlLeads.AddHeader("Authorization", "Zoho-oauthtoken " + Token_Work);
            requesturlLeads.AddHeader("Content-Type", "application/json");
            requesturlLeads.AddHeader("Cookie", "1a99390653=fa937bf8820a337da6a65156d344d3c2; 1ccad04dca=d29e417f368f50fa25b6be760117403f; _zcsr_tmp=3e2a1e6c-a084-4095-a53e-6763be3d3252; crmcsr=3e2a1e6c-a084-4095-a53e-6763be3d3252");

            List<Datum> arrayItem = new List<Datum>();
            arrayItem.AddRange(_dati);

            Contact item = new Contact();
            item.data = arrayItem.ToArray();
            var itemSerialize = JsonConvert.SerializeObject(item);
            requesturlLeads.AddParameter("application/json", itemSerialize, ParameterType.RequestBody);
            var responseLeads = client.Execute(requesturlLeads);
            Console.WriteLine(responseLeads.Content);
            Zresponse zresponse = JsonConvert.DeserializeObject<Zresponse>(responseLeads.Content);
            int counter = 0;
            foreach (var _zresponse in zresponse.data)
            {
                if (_zresponse.status != "error")
                {
                    Utenti utenti1 = UtentiRepository.GetUtentiIdClient(arrayItem[counter].Id_Cliente);
                    if (utenti1 != null)
                    {
                        utenti1.ZohoId = _zresponse.details.id;
                        UtentiRepository.UpdateUtentiEmail(utenti1);
                        //AddUpdateCompani(Token_Work, utenti1);
                    }
                }
                counter++;
            }

        }
        private static void DeleteZohoContact(string lista, List<string> ZohoId, string Token_Work)
        {
            //Esta Api realiza deletes
            var client = new RestClient("https://accounts.zoho.eu/oauth/v2/Saten");
            string urlLeads = "https://www.zohoapis.eu/crm/v2/Contacts?ids=" + lista;
            var requesturlLeads = new RestRequest(urlLeads, Method.Delete);
            requesturlLeads.AddHeader("Authorization", "Zoho-oauthtoken " + Token_Work);
            requesturlLeads.AddHeader("Content-Type", "application/json");
            requesturlLeads.AddHeader("Cookie", "1a99390653=fa937bf8820a337da6a65156d344d3c2; 1ccad04dca=d29e417f368f50fa25b6be760117403f; _zcsr_tmp=3e2a1e6c-a084-4095-a53e-6763be3d3252; crmcsr=3e2a1e6c-a084-4095-a53e-6763be3d3252");

            var responseLeads = client.Execute(requesturlLeads);
            Console.WriteLine(responseLeads.Content);

            Zresponse zresponse = JsonConvert.DeserializeObject<Zresponse>(responseLeads.Content);

            int countres = 0;
            foreach (var _zresponse in zresponse.data)
            {
                if (_zresponse.status != "error")
                {
                    //Utenti utenti1 = UtentiRepository.GetUtentiEmail(arrayItem[countres].Email);
                    Utenti utenti1 = UtentiRepository.GetUtentiIdZoho(_zresponse.details.id);
                    if (utenti1 != null)
                    {
                        utenti1.ZohoId = _zresponse.details.id;
                        utenti1.IsDeletedInZoho = false;
                        UtentiRepository.UpdateUtentiDelete(utenti1);
                    }
                }
                countres++;
            }
        }


        private static void AddUpdateCompani(string Token_Work, List<DatumCompanie> _dati)
        {
            var client = new RestClient("https://accounts.zoho.eu/oauth/v2/Saten");

            string urlLeads = "https://www.zohoapis.eu/crm/v2/Accounts/upsert";
            var requesturlLeads = new RestRequest(urlLeads, Method.Post);
            requesturlLeads.AddHeader("Authorization", "Zoho-oauthtoken " + Token_Work);
            requesturlLeads.AddHeader("Content-Type", "application/json");
            requesturlLeads.AddHeader("Cookie", "1a99390653=fa937bf8820a337da6a65156d344d3c2; 1ccad04dca=d29e417f368f50fa25b6be760117403f; _zcsr_tmp=3e2a1e6c-a084-4095-a53e-6763be3d3252; crmcsr=3e2a1e6c-a084-4095-a53e-6763be3d3252");

            List<DatumCompanie> arrayItem = new List<DatumCompanie>();

            arrayItem.AddRange(_dati);

            Companie item = new Companie();
            item.data = arrayItem.ToArray();
            var itemSerialize = JsonConvert.SerializeObject(item);
            requesturlLeads.AddParameter("application/json", itemSerialize, ParameterType.RequestBody);
            var responseLeads = client.Execute(requesturlLeads);
            Console.WriteLine(responseLeads.Content);
            Zresponse zresponse = JsonConvert.DeserializeObject<Zresponse>(responseLeads.Content);
            int counter = 0;
            foreach (var _zresponse in zresponse.data)
            {
                if (_zresponse.status != "error")
                {
                    Utenti utenti1 = CompanieRepository.GetUtentiIdClient(arrayItem[counter].Id_Cliente);
                    if (utenti1 != null)
                    {
                        utenti1.IdZohoAziende = _zresponse.details.id;
                        CompanieRepository.UpdateUtentiCompanie(utenti1);
                    }
                }
                counter++;
            }

        }
        private static void AddUpdateOrder(string Token_Work, List<DatumOrder> _dati)
        {
            var client = new RestClient("https://accounts.zoho.eu/oauth/v2/Saten");

            string urlLeads = "https://www.zohoapis.eu/crm/v2/Sales_Orders/upsert";
            var requesturlLeads = new RestRequest(urlLeads, Method.Post);
            requesturlLeads.AddHeader("Authorization", "Zoho-oauthtoken " + Token_Work);
            requesturlLeads.AddHeader("Content-Type", "application/json");
            requesturlLeads.AddHeader("Cookie", "1a99390653=fa937bf8820a337da6a65156d344d3c2; 1ccad04dca=d29e417f368f50fa25b6be760117403f; _zcsr_tmp=3e2a1e6c-a084-4095-a53e-6763be3d3252; crmcsr=3e2a1e6c-a084-4095-a53e-6763be3d3252");

            List<DatumOrder> arrayItem = new List<DatumOrder>();
            arrayItem.AddRange(_dati);

            Order item = new Order();
            item.data = arrayItem;

            var itemSerialize = JsonConvert.SerializeObject(item);
            requesturlLeads.AddParameter("application/json", itemSerialize, ParameterType.RequestBody);
            var responseLeads = client.Execute(requesturlLeads);

            Console.WriteLine(responseLeads.Content);
            Zresponse zresponse = JsonConvert.DeserializeObject<Zresponse>(responseLeads.Content);
            int counter = 0;

            foreach (var _zresponse in zresponse.data)
            {
                if (_zresponse.status != "error")
                {
                    Ordini data = OrdiniRepository.GetOrderById((int)arrayItem[counter].Id_Ordine);
                    if (data != null)
                    {
                        data.IdZoho = _zresponse.details.id;
                        OrdiniRepository.UpdateOrder(data);
                    }
                }
                counter++;
            }

        }

        private static void DeleteZohoCompanie(string lista, List<string> ZohoId, string Token_Work)
        {
            //Esta Api realiza deletes
            var client = new RestClient("https://accounts.zoho.eu/oauth/v2/Saten");
            string urlLeads = "https://www.zohoapis.eu/crm/v2/Accounts?ids=" + lista;
            var requesturlLeads = new RestRequest(urlLeads, Method.Delete);
            requesturlLeads.AddHeader("Authorization", "Zoho-oauthtoken " + Token_Work);
            requesturlLeads.AddHeader("Content-Type", "application/json");
            requesturlLeads.AddHeader("Cookie", "1a99390653=fa937bf8820a337da6a65156d344d3c2; 1ccad04dca=d29e417f368f50fa25b6be760117403f; _zcsr_tmp=3e2a1e6c-a084-4095-a53e-6763be3d3252; crmcsr=3e2a1e6c-a084-4095-a53e-6763be3d3252");

            var responseLeads = client.Execute(requesturlLeads);
            Console.WriteLine(responseLeads.Content);

            Zresponse zresponse = JsonConvert.DeserializeObject<Zresponse>(responseLeads.Content);

            int countres = 0;
            foreach (var _zresponse in zresponse.data)
            {
                if (_zresponse.status != "error")
                {
                    //Utenti utenti1 = UtentiRepository.GetUtentiEmail(arrayItem[countres].Email);
                    Utenti utenti1 = UtentiRepository.GetUtentiIdZoho(_zresponse.details.id);
                    if (utenti1 != null)
                    {
                        utenti1.ZohoId = _zresponse.details.id;
                        utenti1.IsDeletedInZoho = false;
                        UtentiRepository.UpdateUtentiDelete(utenti1);
                    }
                }
                countres++;
            }
        }

    }
    public class GenerarToken
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string api_domain { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }

    public class Generaterken
    {
        public string access_token { get; set; }
        public string api_domain { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }


    public class Main
    {
        public Customer[] data { get; set; }
    }

    public class Customer
    {
        public string Company { get; set; }
        public string Last_Name { get; set; }
        public string First_Name { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
    }




}
