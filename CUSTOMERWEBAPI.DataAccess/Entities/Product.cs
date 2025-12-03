using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMERWEBAPI.DataAccess.Entities
{
    public class Product
    {
        public int prod_id { get; set; }
        public string prod_desc { get; set; }=string.Empty;
        public string prod_cd { get; set; } = string.Empty;
        public decimal Rate { get; set; }
    }
}
