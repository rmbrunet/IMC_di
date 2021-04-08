using IMC.Domain;

namespace IMC.TaxJarTaxCalculator.Models {
    public class TaxJarLocation {
        public string CountryCode { get; set; }
        public string Zip { get; set; }
        public string StateCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        public static TaxJarLocation MapFromLocation(Location location) {
            return new() {
                Zip = location.Zip,
                State = location.State,
                StateCode = location.StateCode,
                City = location.City,
                Street = location.Street,
                County = location.County,
                Country = location.Country,
                CountryCode = location.CountryCode,
            };
        }
    }
}
