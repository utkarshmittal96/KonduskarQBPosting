using Intuit.Ipp.Exception;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konduskar_QuickBook.Util
{
    public class Logger
    {
        public static void WriteLog(string data)
        {
            try
            {
                int intServerMinsOffset = Convert.ToInt32(ConfigurationManager.AppSettings["ServerOffsetMins"]);
                StreamWriter log;
                string logpath = ConfigurationManager.AppSettings["LogPath"];
                if (!File.Exists(@logpath + "logfile_" + DateTime.Now.AddMinutes(intServerMinsOffset).ToString("dd-MMM-yyy") + ".txt"))
                {
                    log = new StreamWriter(@logpath + "logfile_" + DateTime.Now.AddMinutes(intServerMinsOffset).ToString("dd-MMM-yyy") + ".txt");
                }
                else
                {
                    log = File.AppendText(@logpath + "logfile_" + DateTime.Now.AddMinutes(intServerMinsOffset).ToString("dd-MMM-yyy") + ".txt");
                }

                string currenttime = Util.GetServerDateTime().ToString("dd-MMM-yyy HH:mm:ss.fff");
                if (data != "")
                    log.WriteLine(currenttime + " : " + data);

                log.Close();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        public static void WriteLog(string data, bool toSendMail)
        {
            WriteLog(data);
            if (toSendMail)
            {
                //Email.SendMail(data);
            }
        }

        public static void WriteLog(string heading,string subheading,string data,bool toSendMail)
        {
            string msg = heading + "::" + subheading + "::" + data;
            WriteLog(msg);
            if(toSendMail)
            {
                //Email.SendMail(msg);
            }
        }

        public static void WriteQBExceptonDetailToLog(IdsException iex)
        {
            if(iex != null && iex.InnerException != null)
            {
                System.Collections.ObjectModel.ReadOnlyCollection<IdsError> errorCollection = ((Intuit.Ipp.Exception.IdsException)iex.InnerException).InnerExceptions;
                if ( errorCollection != null && errorCollection.Count > 0 )
                {
                    for (int i=0; i < errorCollection.Count; i++)
                    {
                        IdsError error = errorCollection[i];                        
                        WriteLog("ErrorCode: " + error.ErrorCode + ", Message: " + error.Message + ", Element: " + error.Element + ", Detail: " + error.Detail);
                    }
                }
                
            }
            
        }
        
    }
}
