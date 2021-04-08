using System.Text.Json.Serialization;

namespace IMC.TaxJarTaxCalculator.Models {
    public class TaxJarLineItem {
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("product_tax_code")]
        public string ProductTaxCode { get; set; }
        [JsonPropertyName("unit_price")]
        public decimal UnitPrice { get; set; }
        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }
    }
}
