using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMERWEBAPI.DataAccess.Entities
{
    public class Customer
    {
        public int cust_id { get; set; }
        public string cust_name { get; set; } = string.Empty;
        public string gst_no { get; set; } = string.Empty;
        public string mob_no { get; set;  } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string country { get; set; }= string.Empty;
        public string address { get; set; } = string.Empty;
    }
}
