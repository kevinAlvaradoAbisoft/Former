using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Zoho.API.Authenticator;
using Com.Zoho.API.Authenticator.Store;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Logger;
using static Com.Zoho.API.Authenticator.OAuthToken;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using SDKInitializer = Com.Zoho.Crm.API.Initializer;

namespace SolZoho
{
    public class Initialize
    {
        public static void SDKInitialize()
        {
            /*
            * Create an instance of Logger Class that requires the following
            * Level -> Level of the log messages to be logged. Can be configured by typing Levels "." and choose any level from the list displayed.
            * FilePath -> Absolute file path, where messages need to be logged.
            */
            Logger logger = new Logger.Builder()
            .Level(Logger.Levels.ALL)
            .FilePath("/Users/Documents/csharp_sdk_log.log")
            .Build();

            //Create an UserSignature instance that takes user Email as parameter
            UserSignature user = new UserSignature("roberto.soliz@abisoft.it");

            /*
            * Configure the environment
            * which is of the pattern Domain.Environment
            * Available Domains: USDataCenter, EUDataCenter, INDataCenter, CNDataCenter, AUDataCenter
            * Available Environments: PRODUCTION, DEVELOPER, SANDBOX
            */
            Environment environment = USDataCenter.PRODUCTION;

            /*
           * Create a Token instance that requires the following
           * clientId -> OAuth client id.
           * clientSecret -> OAuth client secret.
           * refreshToken -> REFRESH token.
           * grantToken -> GRANT token.
           * id -> User unique id.
           * redirectURL -> OAuth redirect URL.
           */
            // if ID (obtained from persistence) is available
            Token token = new OAuthToken.Builder()
            .ClientId("1000.2A43O1GB69ISC69FO0QYVUI8A3BQ4Z")
            .ClientSecret("11349e95f8a7189500918c8e3a06b8fbe2beec11b0")
            .RefreshToken("1000.f8b35c3642f00fb3ee66b6d23503e2e7.8f9f4d58d79135a44631ce5da7c995ec")
            .RedirectURL("https://example.com/yourcallback")
            .Build();

            /*
            * Create an instance of DBStore.
            * Host -> DataBase host name. Default "localhost"
            * DatabaseName -> DataBase name. Default "zohooauth"
            * UserName -> DataBase user name. Default "root"
            * Password -> DataBase password. Default ""
            * PortNumber -> DataBase port number. Default "3306"
            * TableName -> Table Name. Default value "oauthtoken"
            */
            //TokenStore tokenstore = new DBStore.Builder().Build();

            TokenStore tokenstore = new DBStore.Builder()
            .Host("localhost")
            .DatabaseName("dbstore")
            .TableName("oautoken")
            .UserName("root")
            .Password("")
            .PortNumber("3306")
            .Build();


            // TokenStore tokenstore = new FileStore("absolute_file_path");

            /*
            * autoRefreshFields
            * if true - all the modules' fields will be auto-refreshed in the background, every    hour.
            * if false - the fields will not be auto-refreshed in the background. The user can manually delete the file(s) or refresh the fields using methods from ModuleFieldsHandler(com.zoho.crm.api.util.ModuleFieldsHandler)
            * 
            * pickListValidation
            * if true - value for any picklist field will be validated with the available values.
            * if false - value for any picklist field will not be validated, resulting in creation of a new value.
            */
            SDKConfig config = new SDKConfig.Builder().AutoRefreshFields(false).PickListValidation(false).Build();

            string resourcePath = "/Users/pc/Documents/csharpsdk-application";

            /**
           * Create an instance of RequestProxy class that takes the following parameters
           * Host -> Host
           * Port -> Port Number
           * User -> User Name
           * Password -> Password
           * UserDomain -> User Domain
           */
            ////RequestProxy requestProxy = new RequestProxy.Builder()
            ////.Host("proxyHost")
            ////.Port(45454)
            ////.User("proxyUser")
            ////.Password("password")
            ////.UserDomain("userDomain")
            ////.Build();


            /*
           * The initialize method of Initializer class that takes the following arguments
           * User -> UserSignature instance
           * Environment -> Environment instance
           * Token -> Token instance
           * Store -> TokenStore instance
           * SDKConfig -> SDKConfig instance
           * ResourcePath -> resourcePath -A String
           * Logger -> Logger instance
           * RequestProxy -> RequestProxy instance
           */
            // Set the following in InitializeBuilder
            new SDKInitializer.Builder()
            .User(user)
            .Environment(environment)
            .Token(token)
            .Store(tokenstore)
            .SDKConfig(config)
            .ResourcePath(resourcePath)
            .Logger(logger)
            //.RequestProxy(requestProxy)
            .Initialize();


        }
    }
}
