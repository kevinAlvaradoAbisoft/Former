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
using AppWithPostman.Helpers;

namespace AppWithPostman
{
    class Program
    {
        const string ACCOUNTS_URL = "https://accounts.zoho.eu/oauth/v2/token";
        static string Token_Refresh = "";
        static string Token_Work = "";
        static int Main(string[] args)
        {
            ZohoAPIHelper zohoAPIHelper = new ZohoAPIHelper();
            Token_Work =  zohoAPIHelper.ObtainTokens();
            Token_Refresh = zohoAPIHelper.TokenRefresh;

            #region Add users to the users table UserZoho
            try
            {
                List<UserDTO> getusers = UserZohoRepository.GetUsersZoho();
                if(getusers.Count > 0)
                {
                    var data = UserZohoRepository.AddUserZoho(getusers);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error add Users to the users table UserZoho: {ex.Message}");
                return ex.HResult;
            }
            #endregion

            #region Adding and updating Azienda
            try
            {
                List<UserDTO> utentiCompanie = CompanieRepository.GetUtentiCompanie();
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

                                    Phone = _utente.Phone,
                                    //Account_Name = (_utente.First_Name + " " + _utente.Last_Name).Trim(),
                                    Account_Name = _utente.RagSoc,
                                    s = "Disponibile",
                                    approval_state = "approved",
                                    Id_Cliente = _utente.IdUser
                                }
                            );
                        numeroContattiCompanie++;
                    }
                    else
                    {
                        Console.WriteLine($"Aziende: AddUpdate {_datiCompanie.Count} of {utentiCompanie.Count} records");
                        CompanieHelper.AddUpdateCompanie(Token_Work, _datiCompanie);
                        numeroContattiCompanie = 1;
                        _datiCompanie.Clear();

                        _datiCompanie.Add(
                            new DatumCompanie()
                            {
                                state = "save",
                                approved = true,
                                Enrich_Status__s = "Disponibile",
                                editable = true,                                
                                Phone = _utente.Phone,
                                //Account_Name = (_utente.First_Name + " " + _utente.Last_Name).Trim(),
                                Account_Name = _utente.RagSoc,
                                s = "Disponibile",
                                approval_state = "approved",
                                Id_Cliente = _utente.IdUser//
                            }
                        );
                    }

                }
                if (_datiCompanie.Count > 0)
                {
                    Console.WriteLine($"Aziende: AddUpdate the last {_datiCompanie.Count} records of {utentiCompanie.Count} ");
                    CompanieHelper.AddUpdateCompanie(Token_Work, _datiCompanie);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding/updating Aziende: {ex.Message}");
                return ex.HResult;
            }
            #endregion       

            #region Adding and updating Contatti
            try
            {
                List<UserDTO> utenti = UtentiRepository.GetUtenti();
                List<Datum> _dati = new List<Datum>();
                int numeroContatti = 1;
                foreach (var _utente in utenti)
                {
                    if (numeroContatti <= 10)
                    {
                        _dati.Add(
                                new Datum()
                                {

                                    Full_Name = (_utente.First_Name + " " + _utente.Last_Name).Trim(), //aumente ya que me dio error que es campo necesrio
                                    Email = _utente.Email,
                                    First_Name = _utente.First_Name.Trim(),
                                    //Last_Name = _utente.Last_Name.Trim(),
                                    Last_Name = string.IsNullOrEmpty(_utente.Last_Name.Trim()) ? _utente.First_Name.Trim() : _utente.Last_Name.Trim(),
                                    Phone = _utente.Phone,
                                    PEC = _utente.Pec,
                                    SDI = _utente.Sdi,
                                    Codice_Fiscale = string.IsNullOrEmpty(_utente.Codice_Fiscale) ? "0" : _utente.Codice_Fiscale.Substring(0, 9),
                                    Id_Cliente = _utente.IdUser,
                                    Partita_Iva = string.IsNullOrEmpty(_utente.Partita_Iva) ? "0" : _utente.Partita_Iva.Substring(0,9),
                                    //Partita_Iva = "0",
                                    Mobile = _utente.Mobile,
                                    Tipologia = "",  
                                    E_mail_amministrazione="",//no debe ser null
                                    Account_Name = new AccountNameCompanie
                                    {
                                        //name = (_utente.First_Name + " " + _utente.Last_Name).Trim(),
                                        name = _utente.RagSoc,
                                        id = _utente.IdZohoAziende
                                    }
                                }
                            );
                        numeroContatti++;
                    }
                    else
                    {
                        Console.WriteLine(String.Format("Contact: Adding/updating {0} of {1} records", _dati.Count, utenti.Count));
                        ContactHelper.AddUpdateContact(Token_Work, _dati);
                        numeroContatti = 1;
                        _dati.Clear();

                        _dati.Add(
                            new Datum()
                            {

                                Full_Name = (_utente.First_Name + " " + _utente.Last_Name).Trim(), //aumente ya que me dio error que es campo necesrio
                                Email = _utente.Email,
                                First_Name = _utente.First_Name.Trim(),
                                Last_Name = string.IsNullOrEmpty(_utente.Last_Name.Trim()) ? _utente.First_Name.Trim() : _utente.Last_Name.Trim(),
                                Phone = _utente.Phone,
                                PEC = _utente.Pec,
                                SDI = _utente.Sdi,
                                Codice_Fiscale = string.IsNullOrEmpty(_utente.Codice_Fiscale) ? "0" : _utente.Codice_Fiscale.Substring(0, 9),
                                Id_Cliente = _utente.IdUser,
                                Partita_Iva = string.IsNullOrEmpty(_utente.Partita_Iva) ? "0" : _utente.Partita_Iva.Substring(0,9),
                                //Partita_Iva = "0",
                                Mobile = _utente.Mobile,
                                Tipologia="",
                                E_mail_amministrazione="",
                                Account_Name = new AccountNameCompanie
                                {
                                    //name = (_utente.First_Name + " " + _utente.Last_Name).Trim(),
                                    name = _utente.RagSoc,
                                    id = _utente.IdZohoAziende
                                }
                            }
                        );
                    }

                }
                if (_dati.Count > 0)
                {
                    Console.WriteLine(String.Format("Contact: Adding/updating the last {0} records of {1}", _dati.Count, utenti.Count));
                    ContactHelper.AddUpdateContact(Token_Work, _dati);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding/updating contacts: {ex.Message}");
                return ex.HResult;
            }
            #endregion


            #region Deleting Aziende
            try
            {
                List<string> arrayItem1 = new List<string>();
                List<UserDTO> utenti2 = CompanieRepository.GetCompanieDelete();
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
                            Console.WriteLine(String.Format("Aziende: Deleting {0} records", utenti2.Count));
                            CompanieHelper.DeleteZohoCompanie(lista, arrayItem1, Token_Work);

                        }
                        _count++;
                    }
                    if (!string.IsNullOrEmpty(lista))
                    {
                        Console.WriteLine(String.Format("Aziende: Deleting {0} records", utenti2.Count));
                        CompanieHelper.DeleteZohoCompanie(lista, arrayItem1, Token_Work);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting Aziende: {ex.Message}");
                return ex.HResult;
            }
            #endregion

            #region Add orders to the table OrderZoho
            try
            {
                List<OrderDTO> getOrders = OrderZohoRepository.GetOrdersZoho();
                if (getOrders.Count > 0)
                {
                    var data = OrderZohoRepository.AddOrderZoho(getOrders);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error add Orders to the table OrderZoho: {ex.Message}");
                return ex.HResult;
            }
            #endregion

            #region Adding Ordini
            try
            {
                List<OrderDTO> ordini = OrdiniRepository.GetOrders();
                List<DatumOrder> _datiOrder = new List<DatumOrder>();
                List<ProductDetails> productList = new List<ProductDetails>();

                int numberOrder = 1;

                if (ordini.Count > 0)
                {
                    var data = ordini[0];

                    Console.WriteLine($"Ordini: AddUpdate {ordini.Count} ordini");
                    foreach (var _order in ordini)
                    {
                        productList.Add(new ProductDetails
                        {
                            product = new Product
                            {
                                Product_Code = "Offset",
                                name = "Offset",
                                id = "517570000000607003"
                            },
                            //quantity = _order.qta,
                            quantity = 1,
                            Discount = (int)_order.Sconto,
                            total_after_discount = 0,
                            net_total = 0,
                            book = null,
                            Tax = null,
                            list_price = (int)_order.PrezzoCalcolatoNetto,
                            unit_price = null,
                            quantity_in_stock = -1,
                            total = (int)_order.Total_Order,
                            //total = 44,
                            product_description = null,
                        });

                        _datiOrder.Add(
                                new DatumOrder()
                                {
                                    currency_symbol = "€",
                                    Quantit = _order.qta,
                                    field_states = null,
                                    Tax = (int)_order.Total_Iva,
                                    Anteprima = !string.IsNullOrEmpty(_order.Anteprima) ? $"https://tipografiaformer.it/ordini/{_order.IdOrder}/{_order.Anteprima}":"",
                                    Sorgente_Fronte = !string.IsNullOrEmpty(_order.Sorgente_Fronte) ? $"https://tipografiaformer.it/ordini/{_order.IdOrder}/{_order.Sorgente_Fronte}":"",
                                    state = "save",
                                    converted = false,
                                    process_flow = false,
                                    Deal_Name = null,
                                    Billing_Country = null,
                                    Carrier = _order.Carrier,
                                    approved = true,
                                    Quote_Name = null,
                                    Status = "Registrato",
                                    Grand_Total = (int)_order.Total_Order,

                                    Billing_Street = null,
                                    Adjustment = (int)(_order.PrezzoCalcolatoNetto - _order.Sconto),
                                    editable = true,
                                    Billing_Code = null,
                                    Product_Details = productList.ToArray(),
                                    Sorgente_Retro = !string.IsNullOrEmpty(_order.Sorgente_Retro) ? $"https://tipografiaformer.it/ordini/{_order.IdOrder}/{_order.Sorgente_Retro}/":"",
                                    //Id_Ordine = 14589,
                                    Excise_Duty = null,
                                    Shipping_City = null,
                                    Shipping_Country = null,
                                    Shipping_Code = null,
                                    Billing_City = null,
                                    Purchase_Order = null,

                                    Shipping_Street = null,
                                    Description = null,
                                    Discount = 0,
                                    Shipping_State = null,

                                    review = null,
                                    Account_Name = _order.Account_Name,
                                    Tipologia_di_consegna = "Ritiro da Cliente",
                                    Sales_Commission = null,

                                    Due_Date = null,
                                    //Data_di_consegna = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd"),
                                    Data_di_consegna = _order.DataPrevConsegna!=null? _order.DataPrevConsegna.Value.Year.ToString("0000")+"-"+ _order.DataPrevConsegna.Value.Month.ToString("00")+"-"+ _order.DataPrevConsegna.Value.Day.ToString("00"): _order.DataCambioStato.Value.Year.ToString("0000")+"-"+ _order.DataCambioStato.Value.Month.ToString("00")+"-"+ _order.DataCambioStato.Value.Day.ToString("00"),    
                                    Terms_and_Conditions = null,
                                    Sub_Total = (int)_order.Sub_Total,
                                    //Data_di_inserimento = DateTime.Now.ToString("yyyy-MM-dd"),
                                    Data_di_inserimento = _order.DataIns.Value.Year.ToString("0000")+"-"+ _order.DataIns.Value.Month.ToString("00")+"-"+ _order.DataIns.Value.Day.ToString("00"),
                                    Subject = _order.qta + " " + _order.Subject,
                                    orchestration = false,
                                    Contact_Name = null,
                                    Id_Ordine = _order.IdOrder
                                }
                            );
                        OrderHelper.AddUpdateOrder(Token_Work, _datiOrder);
                        numberOrder = 1;
                        _datiOrder.Clear();
                        productList.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Ordini: {ex.Message}");
                return ex.HResult;
            }
            #endregion

            Console.WriteLine("Finishing process");
            return 0;

            
        }

    }
}
