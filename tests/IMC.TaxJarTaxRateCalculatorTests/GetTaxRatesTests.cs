using IMC.Domain;
using IMC.TaxJarTaxCalculator;
using FluentValidation;
using Moq;
using Moq.Contrib.HttpClient;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace TaxJarTaxRateCalculatorTests {
    public class GetTaxRatesTests {

        [Theory]
        [InlineData("33029")]
        [InlineData("33029-6009")]
        public async Task Return_valid_response_for_FL_Broward_County(string zipCode) {

            var response = new {
                rate = new {
                    zip = zipCode,
                    country = "US",
                    country_rate = "0.0",
                    state = "FL",
                    state_rate = "0.06",
                    county = "BROWARD",
                    county_rate = "0.01",
                    city_rate = "0.0",
                    combined_district_rate = "0.0",
                    combined_rate = "0.07",
                    freight_taxable = false
                }
            };

            var json = JsonSerializer.Serialize<dynamic>(response);

            string baseUrl = "https://test.com";
            var handler = new Mock<HttpMessageHandler>();
            handler
                .SetupRequest(HttpMethod.Get, $"{baseUrl}/rates/{zipCode}")
                .ReturnsResponse(json, "application/json");

            var client = handler.CreateClient();
            client.BaseAddress = new Uri(baseUrl);

            TaxJarTaxCalculator calculator = new(client);
            var location = new Location() { Zip = zipCode };

            var taxRates = await calculator.GetTaxRates(location);

            Assert.Equal(zipCode, taxRates.Zip);
            Assert.Equal("US", taxRates.Country);
            Assert.Equal("FL", taxRates.State);
            Assert.Equal("BROWARD", taxRates.County);
            Assert.Equal(0.06m, taxRates.StateRate, 2);
            Assert.Equal(0.01m, taxRates.CountyRate, 2);
            Assert.Equal(0.07m, taxRates.CombinedRate, 2);
            Assert.False(taxRates.FreightTaxable);

        }

        [Fact]
        public async Task Throws_validation_exception_when_zip_code_is_absent() {

            var handler = new Mock<HttpMessageHandler>();
            var client = handler.CreateClient();

            TaxJarTaxCalculator calculator = new(client);
            var location = new Location() { Zip = null };
            ValidationException ex = await Assert.ThrowsAsync<ValidationException>(() => calculator.GetTaxRates(location));
            Assert.Contains("Zip Code is mandatory", ex.Message);
        }

        [Fact]
        public async Task Throws_validation_exception_when_zip_code_is_not_USA_and_CountryCode_absent () {

            var handler = new Mock<HttpMessageHandler>();
            var client = handler.CreateClient();

            TaxJarTaxCalculator calculator = new(client);

            var location = new Location() { Zip = "K1N 1G8", CountryCode = null };

            ValidationException ex = await Assert.ThrowsAsync<ValidationException>(() => calculator.GetTaxRates(location));
            Assert.Contains("Country Code is mandatory for Locations different than USA", ex.Message);
        }

        [Fact]
        public async Task Throws_validation_exception_when_CountryCode_length_greater_than_two() {

            var handler = new Mock<HttpMessageHandler>();
            var client = handler.CreateClient();

            TaxJarTaxCalculator calculator = new(client);

            var location = new Location() { Zip = "K1N 1G8", CountryCode = "CAN" };

            ValidationException ex = await Assert.ThrowsAsync<ValidationException>(() => calculator.GetTaxRates(location));
            Assert.Contains("Country Code must be two characters long", ex.Message);
        }

    }
}
