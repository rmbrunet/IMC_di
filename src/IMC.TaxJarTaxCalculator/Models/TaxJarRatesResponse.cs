using System.Text.Json.Serialization;

namespace IMC.TaxJarTaxCalculator.Models {
    public class TaxJarRatesResponse {
        [JsonPropertyName("rate")]
        public TaxJarTaxRates Rates { get; set; }

    }
}
