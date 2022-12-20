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
    public class OrderHelper
    {
        public static bool AddUpdateOrder(string Token_Work, List<DatumOrder> _dati)
        {
            bool retVal = false;
            var client = new RestClient("https://accounts.zoho.eu/oauth/v2/Saten");

            //string urlLeads = "https://www.zohoapis.eu/crm/v2/Sales_Orders/upsert";
            string urlLeads = "https://www.zohoapis.eu/crm/v2/Sales_Orders";
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

            if (zresponse.data != null)
            {
                foreach (var _zresponse in zresponse.data)
                {
                    if (_zresponse.status != "error")
                    {
                        Ordini data = OrdiniRepository.GetOrderById((int)arrayItem[counter].Id_Ordine);
                        if (data != null)
                        {
                            data.IdZoho = _zresponse.details.id;
                            OrdiniRepository.UpdateOrder(data);
                            retVal &= true;
                        }
                    }
                    counter++;
                }
            }

            return retVal;
        }
    }
}
