using IMC.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace IMC.TaxJarTaxCalculator.Models {
    public class TaxJarOrder {

        [JsonIgnore]
        public string Id { get; set; }

        [JsonPropertyName("from_city")]
        public string FromCity { get; set; }

        [JsonPropertyName("from_country")]
        public string FromCountry { get; set; }

        [JsonPropertyName("from_state")]
        public string FromState { get; set; }

        [JsonPropertyName("from_zip")]
        public string FromZip { get; set; }

        [JsonPropertyName("from_street")]
        public string FromStreet { get; set; }

        [JsonPropertyName("to_city")]
        public string ToCity { get; set; }

        // Required 
        [JsonPropertyName("to_country")]
        public string ToCountry { get; set; }

        //Required for Country US or CA
        [JsonPropertyName("to_state")]
        public string ToState { get; set; }

        // Required for Country = "US"
        [JsonPropertyName("to_zip")]
        public string ToZip { get; set; }

        [JsonPropertyName("to_street")]
        public string ToStreet { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("customer_id")]
        public string CustomerId { get; set; }

        [JsonPropertyName("exemptiom_type")]
        public string ExemptionType { get; set; }

        //Required
        [JsonPropertyName("shipping")]
        public decimal Shipping { get; set; }

        [JsonPropertyName("nexus_addresses")]
        public List<TaxJarAddress> NexusAddresses { get; set; }

        [JsonPropertyName("line_items")]
        public List<TaxJarLineItem> LineItems { get; set; }

        public static TaxJarOrder MapFromOrder(Order order) {
            return new() {
                Id = order.Id,
                CustomerId = order.CustomerId,

                FromStreet = order.AddressFrom.Street,
                FromCity = order.AddressFrom.City,
                FromState = order.AddressFrom.StateCode,
                FromCountry = order.AddressFrom.CountryCode,
                FromZip = order.AddressFrom.Zip,

                ToStreet = order.AddressTo.Street,
                ToCity = order.AddressTo.City,
                ToState = order.AddressTo.StateCode,
                ToCountry = order.AddressTo.CountryCode,
                ToZip = order.AddressTo.Zip,

                Shipping = order.Shipping,

                LineItems = order.LineItems.Select(li => new TaxJarLineItem { 
                    Id = li.Id,
                    Discount = li.Discount, 
                    ProductTaxCode = li.ProductTaxCode, 
                    Quantity = li.Quantity, 
                    UnitPrice = li.UnitPrice  
                }).ToList()
            };

        }

    }
}
