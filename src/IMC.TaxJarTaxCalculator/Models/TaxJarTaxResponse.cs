using System.Text.Json.Serialization;

namespace IMC.TaxJarTaxCalculator.Models {
    public class TaxJarTaxResponse {
        [JsonPropertyName("tax")]
        public TaxJarTax Tax { get; set; }
    }
}
