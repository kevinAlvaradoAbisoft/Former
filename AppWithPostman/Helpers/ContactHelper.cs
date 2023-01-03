using AppWithPostman.DTO;
using AppWithPostman.Repository;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.Helpers
{
    public class ContactHelper
    {
        public static void AddUpdateContact(string Token_Work, List<Datum> _dati)
        {
            var client = new RestClient("https://accounts.zoho.eu/oauth/v2/Saten");

            string urlLeads = "https://www.zohoapis.eu/crm/v2/Contacts/upsert";
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
            Zresponse zresponse = JsonConvert.DeserializeObject<Zresponse>(responseLeads.Content);
            int counter = 0;
            foreach (var _zresponse in zresponse.data)
            {
                if (_zresponse.status != "error")
                {
                    UserZoho utenti1 = UtentiRepository.GetUtentiIdClient(arrayItem[counter].IdUt);
                    if (utenti1 != null)
                    {
                        utenti1.IdZoho = _zresponse.details.id;
                        UtentiRepository.UpdateUtentiEmail(utenti1);
                        //AddUpdateCompani(Token_Work, utenti1);
                    }
                }
                counter++;
            }

        }
        public static void DeleteZohoContact(string lista, List<string> ZohoId, string Token_Work)
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
                    UserZoho utenti1 = UtentiRepository.GetUtentiIdZoho(_zresponse.details.id);
                    if (utenti1 != null)
                    {
                        utenti1.IdZoho = _zresponse.details.id;
                        utenti1.IsDeletedInZoho = false;
                        UtentiRepository.UpdateUtentiDelete(utenti1);
                    }
                }
                countres++;
            }
        }
    }
}
