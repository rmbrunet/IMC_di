using FluentValidation;
using IMC.Domain;
using IMC.TaxJarTaxCalculator;
using Moq;
using Moq.Contrib.HttpClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace TaxJarTaxRateCalculatorTests {
    public class GetTaxForOrderTests {

        [Fact]
        public async Task Returned_OrderID_equals_originating_Order_Id() {

            var response = new {
                tax = new {
                    order_total_amount = 16.5m,
                    shipping = 1.5m,
                    taxable_amount = 15m,
                    amount_to_collect = 1.35m,
                    rate = 0.09m,
                    has_nexus = true,
                    freight_taxable = false,
                    jurisdictions = new {
                        country = "US",
                        state = "CA",
                        county = "LOS ANGELES",
                        city = "LOS ANGELES"
                    }
                }
            };

            var json = JsonSerializer.Serialize<dynamic>(response);

            string baseUrl = "https://test.com";
            var handler = new Mock<HttpMessageHandler>();
            handler
                .SetupRequest(HttpMethod.Post, $"{baseUrl}/taxes")
                .ReturnsResponse(json, "application/json");

            var client = handler.CreateClient();
            client.BaseAddress = new Uri(baseUrl);

            TaxJarTaxCalculator calculator = new(client);

            Order order = new Order() {
                AddressTo = new Location {
                    City = "Los Angeles",
                    CountryCode = "US",
                    StateCode = "CA", 
                    Zip = "90002",
                    Street = "1335 E 103rd St"
                },
                AddressFrom = new Location {
                    City = "La Jolla",
                    CountryCode = "US",
                    StateCode = "CA",
                    Zip = "92093",
                    Street = "9500 Gilman Drive"
                },
                CustomerId = "1",
                Id = "1",
                Shipping = 1.5m,
                LineItems = new List<LineItem> {
                    new LineItem {
                        Id = "1",
                        Quantity = 1,
                        UnitPrice = 15m,
                        Discount = 0,
                        ProductTaxCode = "20010"
                    }
                }
            };

            OrderTax tax = await calculator.GetSalesTax(order);

            Assert.Equal(order.Id, tax.OrderId);
            Assert.Equal(order.CustomerId, tax.CustomerId);
            Assert.Equal(response.tax.amount_to_collect, tax.AmountToCollect);
            Assert.Equal(response.tax.order_total_amount, tax.OrderTotalAmount);
            Assert.Equal(response.tax.rate, tax.Rate);
            Assert.Equal(response.tax.freight_taxable, tax.FreightTaxable);
        }

        [Fact]
        public async Task Throws_Validation_execption_when_zipcode_is_missing_for_US() {

            var response = new {
                tax = new {
                    order_total_amount = 16.5m,
                    shipping = 1.5m,
                    taxable_amount = 15m,
                    amount_to_collect = 1.35m,
                    rate = 0.09m,
                    has_nexus = true,
                    freight_taxable = false,
                    jurisdictions = new {
                        country = "US",
                        state = "CA",
                        county = "LOS ANGELES",
                        city = "LOS ANGELES"
                    }
                }
            };

            var json = JsonSerializer.Serialize<dynamic>(response);

            string baseUrl = "https://test.com";
            var handler = new Mock<HttpMessageHandler>();
            handler
                .SetupRequest(HttpMethod.Post, $"{baseUrl}/taxes")
                .ReturnsResponse(json, "application/json");

            var client = handler.CreateClient();
            client.BaseAddress = new Uri(baseUrl);

            TaxJarTaxCalculator calculator = new(client);

            Order order = new Order() {
                AddressTo = new Location {
                    City = "Los Angeles",
                    CountryCode = "US",
                    StateCode = "CA",
                    //Zip = "90002",
                    Street = "1335 E 103rd St"
                },
                AddressFrom = new Location {
                    City = "La Jolla",
                    CountryCode = "US",
                    StateCode = "CA",
                    Zip = "92093",
                    Street = "9500 Gilman Drive"
                },
                CustomerId = "1",
                Id = "1",
                Shipping = 1.5m,
                LineItems = new List<LineItem> {
                    new LineItem {
                        Id = "1",
                        Quantity = 1,
                        UnitPrice = 15m,
                        Discount = 0,
                        ProductTaxCode = "20010"
                    }
                }
            };

            ValidationException ex = await Assert.ThrowsAsync<ValidationException>(() => calculator.GetSalesTax(order));
            Assert.Contains("Zip Code is mandatory for US", ex.Message);
        }

        [Fact]

        public async Task Throws_validation_exception_when_state_code_is_missing_for_US() {

            var response = new {
                tax = new {
                    order_total_amount = 16.5m,
                    shipping = 1.5m,
                    taxable_amount = 15m,
                    amount_to_collect = 1.35m,
                    rate = 0.09m,
                    has_nexus = true,
                    freight_taxable = false,
                    jurisdictions = new {
                        country = "US",
                        state = "CA",
                        county = "LOS ANGELES",
                        city = "LOS ANGELES"
                    }
                }
            };

            var json = JsonSerializer.Serialize<dynamic>(response);

            string baseUrl = "https://test.com";
            var handler = new Mock<HttpMessageHandler>();
            handler
                .SetupRequest(HttpMethod.Post, $"{baseUrl}/taxes")
                .ReturnsResponse(json, "application/json");

            var client = handler.CreateClient();
            client.BaseAddress = new Uri(baseUrl);

            TaxJarTaxCalculator calculator = new(client);

            Order order = new Order() {
                AddressTo = new Location {
                    City = "Los Angeles",
                    CountryCode = "US",
                    //StateCode = "CA",
                    Zip = "90002",
                    Street = "1335 E 103rd St"
                },
                AddressFrom = new Location {
                    City = "La Jolla",
                    CountryCode = "US",
                    StateCode = "CA",
                    Zip = "92093",
                    Street = "9500 Gilman Drive"
                },
                CustomerId = "1",
                Id = "1",
                Shipping = 1.5m,
                LineItems = new List<LineItem> {
                    new LineItem {
                        Id = "1",
                        Quantity = 1,
                        UnitPrice = 15m,
                        Discount = 0,
                        ProductTaxCode = "20010"
                    }
                }
            };

            ValidationException ex = await Assert.ThrowsAsync<ValidationException>(() => calculator.GetSalesTax(order));
            Assert.Contains("State Code is mandatory for US and Canada", ex.Message);
        }
    }
}
