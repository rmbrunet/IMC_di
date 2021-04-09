namespace IMC.Domain {
    /// <summary>
    /// Represents a collection of tax rates associated with a Location 
    /// </summary>
    public class TaxRates {
        public Location Location { get; set; }
        public decimal CityRate { get; set; }
        public decimal CombinedDistrictRate { get; set; }
        public decimal CombinedRate { get; set; }
        public decimal CountryRate { get; set; }
        public decimal CountyRate { get; set; }
        public bool FreightTaxable { get; set; }
        public decimal StateRate { get; set; }

    }
}
