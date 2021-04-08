namespace IMC.Domain {
    public class TaxRates {
        public string City { get; set; }
        public decimal CityRate { get; set; }
        public decimal CombinedDistrictRate { get; set; }
        public decimal CombinedRate { get; set; }
        public string Country { get; set; }
        public decimal CountryRate { get; set; }
        public string County { get; set; }
        public decimal CountyRate { get; set; }
        public bool FreightTaxable { get; set; }
        public string State { get; set; }
        public decimal StateRate { get; set; }
        public string Zip { get; set; }

    }
}
