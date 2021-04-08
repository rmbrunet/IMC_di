using IMC.Domain;
using System;
using System.Text.Json.Serialization;

namespace IMC.TaxJarTaxCalculator.Models {
    public class TaxJarTaxRates {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("city_rate")]
        public string CityRate { get; set; }

        [JsonPropertyName("combined_district_rate")]
        public string CombinedDistrictRate { get; set; }

        [JsonPropertyName("combined_rate")]
        public string CombinedRate { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("country_rate")]
        public string CountryRate { get; set; }

        [JsonPropertyName("county")]
        public string County { get; set; }

        [JsonPropertyName("county_rate")]
        public string CountyRate { get; set; }

        [JsonPropertyName("freight_taxable")]
        public bool FreightTaxable { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("state_rate")]
        public string StateRate { get; set; }

        [JsonPropertyName("zip")]
        public string Zip { get; set; }

        public TaxRates MapToTaxRates() {
            return new() {
                Zip = Zip,
                Country = Country,
                CountryRate = Convert.ToDecimal(CountryRate),
                City = City,
                CityRate = Convert.ToDecimal(CityRate),
                County = County,
                CountyRate = Convert.ToDecimal(CountyRate),
                State = State,
                StateRate = Convert.ToDecimal(StateRate),
                FreightTaxable = FreightTaxable,
                CombinedRate = Convert.ToDecimal(CombinedRate),
                CombinedDistrictRate = Convert.ToDecimal(CombinedDistrictRate)
            };
        }
    }
}
