using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IMC.TaxJarTaxCalculator.Models {
    public class TaxJarTaxBreakdown {
        [JsonPropertyName("city_tax_collectable")]
        public decimal CityTaxCollectable { get; set; }
        [JsonPropertyName("city_tax_rate")]
        public decimal CityTaxRate { get; set; }
        [JsonPropertyName("city_taxable_amount")]
        public decimal CityTaxableAmount { get; set; }
        [JsonPropertyName("combined_tax_rate")]
        public decimal CombinedTaxRate { get; set; }
        [JsonPropertyName("county_tax_rate")]
        public decimal CountyTaxRate { get; set; }
        [JsonPropertyName("county_taxable_amount")]
        public decimal CountyTaxableAmount { get; set; }

        [JsonPropertyName("line_items")]
        public List<TaxJarTaxLineItem> LineItems { get; set; }

        [JsonPropertyName("special_district_tax_collectable")]
        public decimal SpecialDistrictTaxCollectable { get; set; }
        [JsonPropertyName("special_district_taxable_amount")]
        public decimal SpecialDistrictTaxableAmount { get; set; }
        [JsonPropertyName("special_tax_rate")]
        public decimal SpecialTaxRate { get; set; }

        [JsonPropertyName("state_tax_collectable")]
        public decimal StateTaxCollectable { get; set; }
        [JsonPropertyName("state_tax_rate")]
        public decimal StateTaxRate { get; set; }
        [JsonPropertyName("state_taxable_amount")]
        public decimal StateTaxableAmount { get; set; }

        [JsonPropertyName("tax_collectable")]
        public decimal TaxCollectable { get; set; }
        [JsonPropertyName("taxable_amount")]
        public decimal TaxableAmount { get; set; }

    }
}
