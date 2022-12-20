using AppWithPostman.Repository;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.Helpers
{
    public class ZohoAPIHelper
    {
        string _code;
        string _client_id;
        string _secret_id;
        string _grant_type;
        string _redirect_url;
        string Token_Refresh = "";
        string Token_Work = "";
        tokens token = new tokens();
        RestClient client;

        const string ACCOUNTS_URL = "https://accounts.zoho.eu/oauth/v2/token";

        public String TokenRefresh
        {
            get
            {
                return Token_Refresh;
            }
        }

        public String TokenWork 
        { 
            get 
            { 
                return Token_Work;
            } 
        }
        public ZohoAPIHelper()
        {
            _code = ConfigurationSettings.AppSettings["code"];
            _client_id = ConfigurationSettings.AppSettings["client_id"];
            _secret_id = ConfigurationSettings.AppSettings["secret_id"];
            _grant_type = ConfigurationSettings.AppSettings["grant_type"];
            _redirect_url = ConfigurationSettings.AppSettings["redirect_url"];
            client = new RestClient(ACCOUNTS_URL);
        }

        public string ObtainTokens()
        {
            Token_Refresh = "";
            Token_Work = "";
            token = TokenRepository.GetToken();

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
                if ((timetoken.Value.Hours * 60 + timetoken.Value.Minutes) < 15)
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

            return Token_Work;
        }

    }
}
