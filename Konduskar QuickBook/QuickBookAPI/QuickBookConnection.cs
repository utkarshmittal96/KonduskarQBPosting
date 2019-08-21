using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intuit.Ipp.Core;
using Intuit.Ipp.Security;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Data;
using System.Configuration;
using System.Threading;
using System.Net;
using Intuit.Ipp.OAuth2PlatformClient;
using Intuit.Ipp.Exception;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Konduskar_QuickBook.Util;

namespace Konduskar_QuickBook.QuickBookAPI
{
   

    public class QuickBookConnection
    {
        static OAuth2Client oauthClient = null;
        public static Dictionary<string, string> tokenDict = new Dictionary<string, string>();
        public static string pathFile = "";
        static int counter = 0;

        static IConfigurationRoot builder;
        public QuickBookConnection(string path)
        {
            Logger.WriteLog("Entry6");
            string msg = Directory.GetCurrentDirectory();
            string tokenpath = ConfigurationManager.AppSettings["TokenPath"];
            Logger.WriteLog(msg);
            Logger.WriteLog(tokenpath);
            //AuthorizationKeysQBO.tokenFilePath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()))), Assembly.GetCallingAssembly().GetName().Name, "TokenStore.json");
            AuthorizationKeyQbo.tokenFilePath = Path.Combine(tokenpath, "TokenStore.json");
           // AuthorizationKeyQbo.tokenFilePath = Path.GetFullPath(tokenpath);
            //AuthorizationKeyQbo.tokenFilePath = Path.Combine("E:\Konduskar QuickBook Posting\Konduskar QuickBook\Konduskar QuickBook\App.config", "TokenStore.json");
            string name = Assembly.GetCallingAssembly().GetName().Name;
            builder = new ConfigurationBuilder()
                 .SetBasePath(tokenpath)
                 .AddJsonFile(path, optional: true, reloadOnChange: true)
                 .Build();


            AuthorizationKeyQbo.accessTokenQBO = builder.GetSection("Oauth2Keys")["AccessToken"];

            AuthorizationKeyQbo.refreshTokenQBO = builder.GetSection("Oauth2Keys")["RefreshToken"];
            AuthorizationKeyQbo.clientIdQBO = builder.GetSection("Oauth2Keys")["ClientId"];
            AuthorizationKeyQbo.clientSecretQBO = builder.GetSection("Oauth2Keys")["ClientSecret"];
            AuthorizationKeyQbo.realmIdIAQBO = builder.GetSection("Oauth2Keys")["RealmId"];
            AuthorizationKeyQbo.redirectUrl = builder.GetSection("Oauth2Keys")["RedirectUrl"];
            //AuthorizationKeyQbo.qboBaseUrl = builder.GetSection("Oauth2Keys")["QBOBaseUrl"];
            AuthorizationKeyQbo.appEnvironment = builder.GetSection("Oauth2Keys")["Environment"];

            counter++;


        }
        public QuickBookConnection() : this("Appsettings.json")
        {

        }
        private static void Initialize()
        {
            QuickBookConnection initializer = new QuickBookConnection();
        }
        public static ServiceContext InitializeServiceContextQbo()
        {
           

            if (counter == 0)
                Initialize();
            else
            {
                //Load the second json file
                Logger.WriteLog("Entry3");
                FileInfo fileinfo = new FileInfo(AuthorizationKeyQbo.tokenFilePath);
                string jsonFile = File.ReadAllText(fileinfo.FullName);
                var jObj = JObject.Parse(jsonFile);
                AuthorizationKeyQbo.accessTokenQBO = jObj["Oauth2Keys"]["AccessToken"].ToString();
                AuthorizationKeyQbo.refreshTokenQBO = jObj["Oauth2Keys"]["RefreshToken"].ToString();
            }

            ServiceContext context = null;
            OAuth2RequestValidator reqValidator = null;
            try
            {

                reqValidator = new OAuth2RequestValidator(AuthorizationKeyQbo.accessTokenQBO);
                context = new ServiceContext(AuthorizationKeyQbo.realmIdIAQBO, IntuitServicesType.QBO, reqValidator);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                context.IppConfiguration.BaseUrl.Qbo = "https://quickbooks.api.intuit.com/";
                //context.IppConfiguration.MinorVersion.Qbo = "37";
                var service = new DataService(context);
                var compinfo = service.FindAll<CompanyInfo>(new CompanyInfo());
                Logger.WriteLog("Entry5");
                return context;
            }
            catch (IdsException ex)
            {
                if (ex.Message == "Unauthorized-401")
                {
                    counter++;
                    if (counter < 4)
                    {
                        Logger.WriteLog("Entry4");
                        var serviceContext11 = Helper.GetNewTokens_ServiceContext();
                        return serviceContext11;
                    }
                    else
                    {
                        throw;
                    }
                   

                }
                else
                {
                    throw;
                }
            }

        }

        public static ServiceContext GetDataServiceContext()
        {
            ServiceContext context = null;
            return context;

        }
    }

   
}