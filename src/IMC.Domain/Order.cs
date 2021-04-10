using System.Collections.Generic;

namespace IMC.Domain {
    public class Order {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string TaxCalculatorId { get; set; }
        public Location AddressTo { get; set; }
        public Location AddressFrom { get; set; }
        public decimal Shipping { get; set; }
        public List<LineItem> LineItems { get; set; }
    }
}
