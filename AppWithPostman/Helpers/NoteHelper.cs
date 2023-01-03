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
    public class NoteHelper
    {
        public static void AddNote(string Token_Work, string Module, string IdModule, string Note)
        {
            var client = new RestClient("https://accounts.zoho.eu/oauth/v2/Saten");

            string urlLeads = $"https://www.zohoapis.eu/crm/v2/{Module}/{IdModule}/Notes";
            var requesturlLeads = new RestRequest(urlLeads, Method.Post);
            requesturlLeads.AddHeader("Authorization", "Zoho-oauthtoken " + Token_Work);
            requesturlLeads.AddHeader("Content-Type", "application/json");
            requesturlLeads.AddHeader("Cookie", "1a99390653=fa937bf8820a337da6a65156d344d3c2; 1ccad04dca=d29e417f368f50fa25b6be760117403f; _zcsr_tmp=3e2a1e6c-a084-4095-a53e-6763be3d3252; crmcsr=3e2a1e6c-a084-4095-a53e-6763be3d3252");

            DatumNote datumNote = new DatumNote
            {
                Note_Title="Note",
                Note_Content=Note,
                Parent_Id=IdModule,
                se_module=Module
            };

            List<DatumNote> arrayItem = new List<DatumNote>();
            arrayItem.Add(datumNote);

            Note item = new Note();
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

                }
                counter++;
            }

        }
    }
}
