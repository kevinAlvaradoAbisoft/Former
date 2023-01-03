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
    public class CompanieHelper
    {
        public static void AddUpdateCompanie(string Token_Work, List<DatumCompanie> _dati)
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
            //Console.WriteLine(responseLeads.Content);
            Zresponse zresponse = JsonConvert.DeserializeObject<Zresponse>(responseLeads.Content);
            int counter = 0;
            foreach (var _zresponse in zresponse.data)
            {
                if (_zresponse.status != "error")
                {
                    UserZoho utenti1 = CompanieRepository.GetUtentiIdClient(Convert.ToInt32(arrayItem[counter].IdUser));
                    if (utenti1 != null)
                    {
                        utenti1.IdZohoAziende = _zresponse.details.id;
                        CompanieRepository.UpdateUtentiCompanie(utenti1);
                    }
                }
                counter++;
            }

        }
        public static void DeleteZohoCompanie(string lista, List<string> ZohoId, string Token_Work)
        {
            //Esta Api realiza deletes
            var client = new RestClient("https://accounts.zoho.eu/oauth/v2/Saten");
            string urlLeads = "https://www.zohoapis.eu/crm/v2/Accounts?ids=" + lista;
            var requesturlLeads = new RestRequest(urlLeads, Method.Delete);
            requesturlLeads.AddHeader("Authorization", "Zoho-oauthtoken " + Token_Work);
            requesturlLeads.AddHeader("Content-Type", "application/json");
            requesturlLeads.AddHeader("Cookie", "1a99390653=fa937bf8820a337da6a65156d344d3c2; 1ccad04dca=d29e417f368f50fa25b6be760117403f; _zcsr_tmp=3e2a1e6c-a084-4095-a53e-6763be3d3252; crmcsr=3e2a1e6c-a084-4095-a53e-6763be3d3252");

            var responseLeads = client.Execute(requesturlLeads);
            //Console.WriteLine(responseLeads.Content);

            Zresponse zresponse = JsonConvert.DeserializeObject<Zresponse>(responseLeads.Content);

            int countres = 0;
            foreach (var _zresponse in zresponse.data)
            {
                if (_zresponse.status != "error")
                {
                    //Utenti utenti1 = UtentiRepository.GetUtentiEmail(arrayItem[countres].Email);
                    UserZoho utenti1 = UtentiRepository.GetUtentiIdZohoByIdZohoAziende(_zresponse.details.id);
                    if (utenti1 != null)
                    {
                        //utenti1.ZohoId = _zresponse.details.id;
                        utenti1.IsDeletedInZoho = false;
                        UtentiRepository.UpdateUtentiDelete(utenti1);
                    }
                }
                countres++;
            }
        }
    }
}
