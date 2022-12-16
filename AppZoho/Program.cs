using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.API.Authenticator.Store;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Logger;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Tags;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using static Com.Zoho.API.Authenticator.OAuthToken;
using static Com.Zoho.Crm.API.Record.RecordOperations;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using ResponseHandler = Com.Zoho.Crm.API.Record.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Record.ResponseWrapper;
using SDKInitializer = Com.Zoho.Crm.API.Initializer;

namespace AppZoho
{
    class Program
    {
        static void Main(string[] args)
        {
             /*
              * Crear una instancia de Logger Class que tome dos parámetros
              * 1 -> Nivel de los mensajes de registro a registrar. Se puede configurar escribiendo Niveles "." y elija cualquier nivel de la lista que se muestra.
              * 2 -> Ruta de archivo absoluta, donde se deben registrar los mensajes.
              */
            Logger logger = Logger.GetInstance(Logger.Levels.ALL, "/Users/pc/Documents/csharp_sdk_log.log");

            //Cree una instancia de UserSignature que tome el correo electrónico del usuario como parámetro
            UserSignature user = new UserSignature("roberto.soliz@abisoft.it");

            /*
            * Configurar el entorno
            * que es del patrón Domain.Environment
            * Dominios disponibles: USDataCenter, EUDataCenter, INDataCenter, CNDataCenter, AUDataCenter
             * Entornos disponibles: PRODUCCIÓN, DESARROLLADOR, SANDBOX
            */
            Environment environment = USDataCenter.DEVELOPER;

            /*
           * Create a Token instance
           * 1 -> OAuth client id.
           * 2 -> OAuth client secret.
           * 3 -> REFRESH/GRANT token.
           * 4 -> Token type(REFRESH/GRANT).
           * 5 -> OAuth redirect URL.
           */
            //  Token token = new OAuthToken("clientId", "clientSecret", "REFRESH/GRANT token", TokenType.REFRESH / GRANT, "redirectURL");

            Token token = new OAuthToken("1000.TOCMOZK2O4ZNC9P19G0908ZVNI46AL", "c577ee734c171866cf6c381ad17c9efd0605a3f3b9", "1000.cfd30ca65e4344f6447a320c46bf6015.1aff244cdf21d526fdcc173ab7620f25", 
                TokenType.GRANT, "https://accounts.zoho.com/oauth/v2/token");


            ////Token token = new OAuthToken("clientId", "clientSecret", "REFRESH/GRANT token", TokenType.REFRESH / GRANT, "redirectURL");

         //   Token token = new OAuthToken("1000.ZYFW9D7QOO0PJ14RSCPI033M26VYOZ", "485f96680e781e3f15caf330d36f1b0aa6540a2c69", "1000.07049bebc52c232beab400008d55d081.594db69df5042389966375e1124c3076",
           // TokenType.REFRESH); //, "https://api-console.zoho.com/client/1000.ZYFW9D7QOO0PJ14RSCPI033M26VYOZ");

            /*
             * Create an instance of TokenStore.
             * 1 -> DataBase host name. Default "localhost"
             * 2 -> DataBase name. Default "zohooauth"
             * 3 -> DataBase user name. Default "root"
             * 4 -> DataBase password. Default ""
             * 5 -> DataBase port number. Default "3306"
             */
            //TokenStore tokenstore = new DBStore();

            ////TokenStore tokenstore = new DBStore("hostName", "dataBaseName", "userName", "password", "portNumber");
            TokenStore tokenstore = new DBStore("localhost", "dbstore", "root", "1Tarechi123", "3306");

            //TokenStore tokenstore = new FileStore("absolute_file_path");

            /*
            * autoRefreshFields
            * if true - all the modules' fields will be auto-refreshed in the background, every    hour.
            * if false - the fields will not be auto-refreshed in the background. The user can manually delete the file(s) or refresh the fields using methods from ModuleFieldsHandler(com.zoho.crm.api.util.ModuleFieldsHandler)
            *
            * pickListValidation
            * if true - value for any picklist field will be validated with the available values.
            * if false - value for any picklist field will not be validated, resulting in creation of a new value.
            */

            SDKConfig config = new SDKConfig.Builder().SetAutoRefreshFields(true).SetPickListValidation(false).Build();

            string resourcePath = "/Users/pc/Documents/csharpsdk-application";

            /*
            * Call static initialize method of Initializer class that takes the arguments
            * 1 -> UserSignature instance
            * 2 -> Environment instance
            * 3 -> Token instance
            * 4 -> TokenStore instance
            * 5 -> SDKConfig instance
            * 6 -> resourcePath - A String
            * 7 -> Logger instance
            */
            SDKInitializer.Initialize(user, environment, token, tokenstore, config, resourcePath, logger);

            String moduleAPIName = "Leads";

            RecordOperations recordOperations = new RecordOperations();

            ParameterMap paramInstance = new ParameterMap();

            paramInstance.Add(GetRecordsParam.APPROVED, "both");

            HeaderMap headerInstance = new HeaderMap();

            DateTimeOffset ifmodifiedsince = new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local));

            headerInstance.Add(GetRecordsHeader.IF_MODIFIED_SINCE, ifmodifiedsince);

            //Call getRecords method
            APIResponse<ResponseHandler> response = recordOperations.GetRecords(moduleAPIName, paramInstance, headerInstance);



            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");

                    return;
                }

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get the object from response
                    ResponseHandler responseHandler = response.Object;

                    if (responseHandler is ResponseWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                        //Get the obtained Record instances
                        List<Com.Zoho.Crm.API.Record.Record> records = responseWrapper.Data;

                        foreach (Com.Zoho.Crm.API.Record.Record record in records)
                        {
                            //Get the ID of each Record
                            Console.WriteLine("Record ID: " + record.Id);

                            //Get the createdBy User instance of each Record
                            User createdBy = record.CreatedBy;

                            //Check if createdBy is not null
                            if (createdBy != null)
                            {
                                //Get the ID of the createdBy User
                                Console.WriteLine("Record Created By User-ID: " + createdBy.Id);

                                //Get the name of the createdBy User
                                Console.WriteLine("Record Created By User-Name: " + createdBy.Name);

                                //Get the Email of the createdBy User
                                Console.WriteLine("Record Created By User-Email: " + createdBy.Email);
                            }

                            //Get the CreatedTime of each Record
                            Console.WriteLine("Record CreatedTime: " + record.CreatedTime);

                            //Get the modifiedBy User instance of each Record
                            User modifiedBy = record.ModifiedBy;

                            //Check if modifiedBy is not null
                            if (modifiedBy != null)
                            {
                                //Get the ID of the modifiedBy User
                                Console.WriteLine("Record Modified By User-ID: " + modifiedBy.Id);

                                //Get the name of the modifiedBy User
                                Console.WriteLine("Record Modified By User-Name: " + modifiedBy.Name);

                                //Get the Email of the modifiedBy User
                                Console.WriteLine("Record Modified By User-Email: " + modifiedBy.Email);
                            }

                            //Get the ModifiedTime of each Record
                            Console.WriteLine("Record ModifiedTime: " + record.ModifiedTime);

                            //Get the list of Tag instance each Record
                            List<Tag> tags = record.Tag;

                            //Check if tags is not null
                            if (tags != null)
                            {
                                foreach (Tag tag in tags)
                                {
                                    //Get the Name of each Tag
                                    Console.WriteLine("Record Tag Name: " + tag.Name);

                                    //Get the Id of each Tag
                                    Console.WriteLine("Record Tag ID: " + tag.Id);
                                }
                            }

                            //To get particular field value 
                            Console.WriteLine("Record Field Value: " + record.GetKeyValue("Last_Name"));// FieldApiName

                            Console.WriteLine("Record KeyValues: ");

                            //Get the KeyValue map
                            foreach (KeyValuePair<string, object> entry in record.GetKeyValues())
                            {
                                string keyName = entry.Key;

                                object value = entry.Value;

                                if (value != null)
                                {
                                    Console.WriteLine("Field APIName : " + keyName + "\tValue : " + JsonConvert.SerializeObject(value));
                                }
                            }
                        }
                    }
                }
            }

        }
    }
}
