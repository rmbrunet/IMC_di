using System.ComponentModel.DataAnnotations;

namespace IMC.Domain {
    public class Location {
        public string Id { get; set; }
        public string CountryCode { get; set; }
        public string Zip { get; set; }
        public string StateCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
    }
}
