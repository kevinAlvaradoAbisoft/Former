
using System;
using Com.Zoho.API.Authenticator;
using Com.Zoho.API.Authenticator.Store;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Logger;
using static Com.Zoho.API.Authenticator.OAuthToken;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using SDKInitializer = Com.Zoho.Crm.API.Initializer;

namespace AppZoho
{
    public  class Initialize
    {

        public static void SDKInitialize()
        {
            /*
            * Create an instance of Logger Class that takes two parameters
            * 1 -> Level of the log messages to be logged. Can be configured by typing Levels "." and choose any level from the list displayed.
            * 2 -> Absolute file path, where messages need to be logged.
            */
            Logger logger = Logger.GetInstance(Logger.Levels.ALL, "/Users/pc/Documents/csharp_sdk_log.log");

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
            * Create a Token instance
            * 1 -> OAuth client id.
            * 2 -> OAuth client secret.
            * 3 -> REFRESH/GRANT token.
            * 4 -> Token type(REFRESH/GRANT).
            * 5 -> OAuth redirect URL.
            */
            //Token token = new OAuthToken("clientId", "clientSecret", "REFRESH/GRANT token", TokenType.REFRESH / GRANT, "redirectURL");


            Token token = new OAuthToken("1000.ZYFW9D7QOO0PJ14RSCPI033M26VYOZ", "485f96680e781e3f15caf330d36f1b0aa6540a2c69", "1000.62243723b094586d1f612c7824c8d84d.630fb01a3773461c781763cabe59ca19", 
                TokenType.REFRESH, "https://accounts.zoho.com/oauth/v2/token");

            /*
                * Create an instance of TokenStore.
                * 1 -> DataBase host name. Default "localhost"
                * 2 -> DataBase name. Default "zohooauth"
                * 3 -> DataBase user name. Default "root"
                * 4 -> DataBase password. Default ""
                * 5 -> DataBase port number. Default "3306"
            */
            //TokenStore tokenstore = new DBStore();

            TokenStore tokenstore = new DBStore("localhost", "zohooauth", "root", "", "3306");

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
            SDKConfig sdkConfig = new SDKConfig.Builder().SetAutoRefreshFields(false).SetPickListValidation(true).Build();

            string resourcePath = "/Users/pc/Documents/csharpsdk-application";

            /**
            * Create an instance of RequestProxy class that takes the following parameters
            * 1 -> Host
            * 2 -> Port Number
            * 3 -> User Name
            * 4 -> Password
            * 5 -> User Domain
            */
            // RequestProxy requestProxy = new RequestProxy("proxyHost", "proxyPort", "proxyUser", "password");

            RequestProxy requestProxy = new RequestProxy("proxyHost", 3521, "proxyUser", "password", "userDomain");

            /*
            * The initialize method of Initializer class that takes the following arguments
            * 1 -> UserSignature instance
            * 2 -> Environment instance
            * 3 -> Token instance
            * 4 -> TokenStore instance
            * 5 -> SDKConfig instance
            * 6 -> resourcePath -A String
            * 7 -> Logger instance
            * 8 -> RequestProxy instance
            */

            // The following are the available initialize methods

            SDKInitializer.Initialize(user, environment, token, tokenstore, sdkConfig, resourcePath);

            SDKInitializer.Initialize(user, environment, token, tokenstore, sdkConfig, resourcePath, logger);

           // SDKInitializer.Initialize(user, environment, token, tokenstore, sdkConfig, resourcePath, requestProxy);

         //   SDKInitializer.Initialize(user, environment, token, tokenstore, sdkConfig, resourcePath, logger, requestProxy);
        }

        //public static void SDKInitialize()
        //{
        //    /*
        //    * Create an instance of Logger Class that requires the following
        //    * Level -> Level of the log messages to be logged. Can be configured by typing Levels "." and choose any level from the list displayed.
        //    * FilePath -> Absolute file path, where messages need to be logged.
        //    */
        //    Logger logger = new Logger.Builder()
        //    .Level(Logger.Levels.ALL)
        //    .FilePath("/Users/user_name/Documents/csharp_sdk_log.log")
        //    .Build();

        //    //Create an UserSignature instance that takes user Email as parameter
        //    UserSignature user = new UserSignature("abc@zoho.com");

        //    /*
        //    * Configure the environment
        //    * which is of the pattern Domain.Environment
        //    * Available Domains: USDataCenter, EUDataCenter, INDataCenter, CNDataCenter, AUDataCenter
        //    * Available Environments: PRODUCTION, DEVELOPER, SANDBOX
        //    */
        //    Environment environment = USDataCenter.PRODUCTION;

        //    /*
        //    * Create a Token instance
        //    * ClientId -> OAuth client id.
        //    * ClientSecret -> OAuth client secret.
        //    * GrantToken -> GRANT token.
        //    * RedirectURL -> OAuth redirect URL.
        //    */
        //    Token token = new OAuthToken.Builder()
        //    .ClientId("clientId")
        //    .ClientSecret("clientSecret")
        //    .GrantToken("GRANT token")
        //    .RedirectURL("redirectURL")
        //    .Build();

        //    /*
        //    * TokenStore can be any of the following
        //    * DB Persistence - Create an instance of DBStore
        //    * File Persistence - Create an instance of FileStore
        //    * Custom Persistence - Create an instance of CustomStore
        //    */

        //    /*
        //    * Create an instance of DBStore.
        //    * Host -> DataBase host name. Default "localhost"
        //    * DatabaseName -> DataBase name. Default "zohooauth"
        //    * UserName -> DataBase user name. Default "root"
        //    * Password -> DataBase password. Default ""
        //    * PortNumber -> DataBase port number. Default "3306"
        //    * TableName -> Table Name. Default value "oauthtoken"
        //    */
        //    //TokenStore tokenstore = new DBStore.Builder().Build();

        //    TokenStore tokenstore = new DBStore.Builder()
        //    .Host("hostName")
        //    .DatabaseName("dataBaseName")
        //    .TableName("tableName")
        //    .UserName("userName")
        //    .Password("password")
        //    .PortNumber("portNumber")
        //    .Build();

        //    // TokenStore tokenstore = new FileStore("absolute_file_path");

        //    /*
        //    * autoRefreshFields
        //    * if true - all the modules' fields will be auto-refreshed in the background, every    hour.
        //    * if false - the fields will not be auto-refreshed in the background. The user can manually delete the file(s) or refresh the fields using methods from ModuleFieldsHandler(com.zoho.crm.api.util.ModuleFieldsHandler)
        //    *
        //    * pickListValidation
        //    * if true - value for any picklist field will be validated with the available values.
        //    * if false - value for any picklist field will not be validated, resulting in creation of a new value.
        //    */
        //    SDKConfig config = new SDKConfig.Builder().AutoRefreshFields(false).PickListValidation(true).Timeout(10).Build();

        //    string resourcePath = "/Users/user_name/Documents/csharpsdk-application";

        //    /**
        //    * Create an instance of RequestProxy class that takes the following parameters
        //    * Host -> Host
        //    * Port -> Port Number
        //    * User -> User Name
        //    * Password -> Password
        //    * UserDomain -> User Domain
        //    */
        //    RequestProxy requestProxy = new RequestProxy.Builder()
        //    .Host("proxyHost")
        //    .Port(proxyPort)
        //    .User("proxyUser")
        //    .Password("password")
        //    .UserDomain("userDomain")
        //    .Build();

        //    /*
        //    * The initialize method of Initializer class that takes the following arguments
        //    * User -> UserSignature instance
        //    * Environment -> Environment instance
        //    * Token -> Token instance
        //    * Store -> TokenStore instance
        //    * SDKConfig -> SDKConfig instance
        //    * ResourcePath -> resourcePath -A String
        //    * Logger -> Logger instance
        //    * RequestProxy -> RequestProxy instance
        //    */

        //    // Set the following in InitializeBuilder
        //    new SDKInitializer.Builder()
        //    .User(user)
        //    .Environment(environment)
        //    .Token(token)
        //    .Store(tokenstore)
        //    .SDKConfig(config)
        //    .ResourcePath(resourcePath)
        //    .Logger(logger)
        //    .RequestProxy(requestProxy)
        //    .Initialize();
        //}
    }
}
