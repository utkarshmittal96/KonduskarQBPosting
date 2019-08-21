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
using Intuit.Ipp.Exception;
using Intuit.Ipp.OAuth2PlatformClient;

using System.Collections.ObjectModel;
using System.Reflection;
using System.IO;

using Newtonsoft.Json;

using Newtonsoft.Json.Linq;
using Konduskar_QuickBook.Util;

namespace Konduskar_QuickBook.QuickBookAPI
{
    public class Helper
    {
        public static ServiceContext GetNewTokens_ServiceContext()
        {

            FileInfo fileinfo = new FileInfo(AuthorizationKeyQbo.tokenFilePath);
            string jsonFile = File.ReadAllText(fileinfo.FullName);
            var jObj = JObject.Parse(jsonFile);
            //string strclientid = ConfigurationManager.AppSettings["clientid"];
            //string strclientsecret = ConfigurationManager.AppSettings["clientsecret"];
            //string strredirectUrl = ConfigurationManager.AppSettings["redirectUrl"];
            //string strenvironment = ConfigurationManager.AppSettings["appEnvironment"];
            //string refreshTokenQBO = ConfigurationManager.AppSettings["AccessTokenSecret"];
            var oauth2Client = new OAuth2Client(AuthorizationKeyQbo.clientIdQBO, AuthorizationKeyQbo.clientSecretQBO, AuthorizationKeyQbo.redirectUrl, AuthorizationKeyQbo.appEnvironment);
            //var oauth2Client = new OAuth2Client(strclientid, strclientsecret, strredirectUrl, strenvironment);
            try
            {
                //var tokenResp = oauth2Client.RefreshTokenAsync(AuthorizationKeyQbo.refreshTokenQBO).Result;
                var tokenResp = oauth2Client.RefreshTokenAsync(AuthorizationKeyQbo.refreshTokenQBO).Result;
                if (tokenResp.AccessToken != null)
                {
                    jObj["Oauth2Keys"]["AccessToken"] = tokenResp.AccessToken;
                    jObj["Oauth2Keys"]["RefreshToken"] = tokenResp.RefreshToken;
                }
                else
                {
                    if (jObj["Oauth2Keys"]["RefreshToken"] != null)
                    {
                        tokenResp = oauth2Client.RefreshTokenAsync(jObj["Oauth2Keys"]["RefreshToken"].ToString()).Result;
                        jObj["Oauth2Keys"]["AccessToken"] = tokenResp.AccessToken;
                        jObj["Oauth2Keys"]["RefreshToken"] = tokenResp.RefreshToken;
                    }

                }
               
            }
            catch (IdsException ex)
            {

                if (jObj["Oauth2Keys"]["RefreshToken"] != null)
                {
                    var tokenResp = oauth2Client.RefreshTokenAsync(jObj["Oauth2Keys"]["RefreshToken"].ToString()).Result;
                    jObj["Oauth2Keys"]["AccessToken"] = tokenResp.AccessToken;
                    jObj["Oauth2Keys"]["RefreshToken"] = tokenResp.RefreshToken;
                }
                else
                {
                    throw ex;
                }
            }


            string output = JsonConvert.SerializeObject(jObj, Formatting.Indented);
            File.WriteAllText(fileinfo.FullName, output);
            //tokenDict.Clear();
            
            var serviceContext = QuickBookConnection.InitializeServiceContextQbo();
            Logger.WriteLog("Entry7");
            return serviceContext;



        }

    }
}
