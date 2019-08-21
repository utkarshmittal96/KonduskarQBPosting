using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konduskar_QuickBook.Util
{
    public class Util
    {
        public static DateTime GetServerDateTime()
        {
            int intServerMinsOffset = 0;
            int.TryParse(ConfigurationManager.AppSettings["ServerOffsetMins"],out intServerMinsOffset);
            return DateTime.Now.AddMinutes(intServerMinsOffset);
        }
    }
}
