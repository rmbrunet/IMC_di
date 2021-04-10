namespace IMC.Domain {
    public class OrderTax {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string TaxCalculatorId { get; set; }
        public decimal AmountToCollect { get; set; }
        public bool FreightTaxable { get; set; }
        public decimal OrderTotalAmount { get; set; }
        public decimal Rate { get; set; }
        public decimal Shipping { get; set; }
        public decimal TaxableAmount { get; set; }
    }
}
