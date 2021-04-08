using System.Text.Json.Serialization;

namespace IMC.TaxJarTaxCalculator.Models {
    public class TaxJarAddress {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        //Required
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("zip")]
        public string Zip { get; set; }
        [JsonPropertyName("state")]
        //Required
        public string State { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("street")]
        public string Street { get; set; }
    }
}
