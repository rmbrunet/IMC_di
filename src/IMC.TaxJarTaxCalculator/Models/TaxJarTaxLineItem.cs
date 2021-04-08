using System.Text.Json.Serialization;

namespace IMC.TaxJarTaxCalculator.Models {
    public class TaxJarTaxLineItem {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("city_amount")]
        public decimal CityAmount { get; set; }
        [JsonPropertyName("city_tax_rate")]
        public decimal CityTaxRate { get; set; }
        [JsonPropertyName("city_taxable_amount")]
        public decimal CityTaxableAmount { get; set; }
        [JsonPropertyName("combined_tax_rate")]
        public decimal CombinedTaxRate { get; set; }
        [JsonPropertyName("county_amount")]
        public decimal CountyAmount { get; set; }
        [JsonPropertyName("county_tax_rate")]
        public decimal CountyTaxRate { get; set; }
        [JsonPropertyName("county_taxable_amount")]
        public decimal CountyTaxableAmount { get; set; }
        [JsonPropertyName("special_district_amount")]
        public decimal SpecialDistrictAmount { get; set; }
        [JsonPropertyName("special_district_taxable_amount")]
        public decimal SpecialDistrictTaxableAmount { get; set; }
        [JsonPropertyName("special_tax_rate")]
        public decimal SpecialTaxRate { get; set; }
        [JsonPropertyName("state_amount")]
        public decimal StateAmount { get; set; }
        [JsonPropertyName("satate_sales_tax_rate")]
        public decimal SatateSalesTaxRate { get; set; }
        [JsonPropertyName("state_taxable_amount")]
        public decimal StateTaxableAmount { get; set; }
        [JsonPropertyName("tax_collectable")]
        public decimal TaxCollectable { get; set; }
        [JsonPropertyName("taxable_amount")]
        public decimal TaxableAmount { get; set; }
    }
}
