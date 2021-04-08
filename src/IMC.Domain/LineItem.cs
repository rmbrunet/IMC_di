using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMC.Domain
{
    public class LineItem
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string ProductTaxCode { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }

}
