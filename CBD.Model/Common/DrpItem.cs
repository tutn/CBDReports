using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBD.Model.Common
{
    public class DrpItem
    {
        public bool Status { get; set; }
        public int Code { set; get; }
        public string Message { set; get; }
        public object Data { get; set; }
        public string Value { set; get; }
        public string Name { set; get; }
    }
}
