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
    }
}
