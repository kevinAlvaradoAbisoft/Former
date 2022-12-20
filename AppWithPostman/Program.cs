﻿using RestSharp;
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
        static void Main(string[] args)
        {
            ZohoAPIHelper zohoAPIHelper = new ZohoAPIHelper();
            Token_Work =  zohoAPIHelper.ObtainTokens();
            Token_Refresh = zohoAPIHelper.TokenRefresh;

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
                CompanieHelper.AddUpdateCompanie(Token_Work, _datiCompanie);
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
                    ContactHelper.AddUpdateContact(Token_Work, _dati);
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
                ContactHelper.AddUpdateContact(Token_Work, _dati);
            }

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
                        CompanieHelper.DeleteZohoCompanie(lista, arrayItem1, Token_Work);

                    }
                    _count++;
                }
                if (!string.IsNullOrEmpty(lista))
                {
                    CompanieHelper.DeleteZohoCompanie(lista, arrayItem1, Token_Work);
                }
            }

            //order
            List<Ordini> ordini = OrdiniRepository.GetOrders();
            List<DatumOrder> _datiOrder = new List<DatumOrder>();
            List<ProductDetails> productList = new List<ProductDetails>();

            int numberOrder = 1;

            var data = ordini[0];

            foreach (var _order in ordini)
            {
                productList.Add(new ProductDetails
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
                            Product_Details = productList.ToArray(),
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
                            Account_Name = new Account_Name
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
                            Contact_Name= null,
                            Id_Ordine = _order.IdOrdine

                        }
                    );
                if(!OrderHelper.AddUpdateOrder(Token_Work, _datiOrder))
                {
                    Token_Work = zohoAPIHelper.ObtainTokens();
                    OrderHelper.AddUpdateOrder(Token_Work, _datiOrder);
                }
                numberOrder = 1;
                _datiOrder.Clear();
                productList.Clear();
            }

        }

    }
}
