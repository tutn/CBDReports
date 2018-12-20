using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBD.Model.Configuration
{
    public class SystemConfiguration
    {
        private static string _prefixc;
        public static string PREFIXC
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_prefixc)) return _prefixc;
                _prefixc = ConfigurationManager.AppSettings["PREFIXC"];
                if (string.IsNullOrWhiteSpace(_prefixc))
                {
                    _prefixc = "__";
                }
                return _prefixc;
            }
        }

        private static List<string> _userNotLdap;
        public static List<string> UserNotLdap
        {
            get
            {
                if (_userNotLdap != null && _userNotLdap.Count > 0)
                {
                    return _userNotLdap;
                }
                var key = ConfigurationManager.AppSettings.AllKeys.FirstOrDefault(x => x.ToUpper() == "USERNOTLDAP");
                if (!string.IsNullOrEmpty(key)) _userNotLdap = ConfigurationManager.AppSettings[key].Split(';').Select(x => x.Trim()).ToList();
                return _userNotLdap;
            }
        }

        private static string _preDefaultPassword;
        public static string PREDEFAULTPASSWORD
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_preDefaultPassword)) return _preDefaultPassword;
                _preDefaultPassword = ConfigurationManager.AppSettings["PREDEFAULTPASSWORD"];
                if (string.IsNullOrWhiteSpace(_preDefaultPassword))
                {
                    _preDefaultPassword = "VPBank";
                }
                return _preDefaultPassword;
            }
        }
    }
}
