using FluentValidation;
using Flurl;
using IMC.Application.Interfaces;
using IMC.Domain;
using IMC.TaxJarTaxCalculator.Models;
using IMC.TaxJarTaxCalculator.Validators;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IMC.TaxJarTaxCalculator {
    public class TaxJarTaxCalculator : ITaxCalculator {
        private readonly HttpClient _httpClient;
        public TaxJarTaxCalculator(HttpClient client) {
            _httpClient = client;
        }

        public async Task<OrderTax> GetSalesTax(Order order) {
            TaxJarOrder taxJarOrder = TaxJarOrder.MapFromOrder(order);

            TarJarOrderValidator validator = new();

            validator.ValidateAndThrow(taxJarOrder);

            var response = await _httpClient.PostAsJsonAsync<TaxJarOrder>("taxes", taxJarOrder);

            response.EnsureSuccessStatusCode();

            TaxJarTaxResponse tjTaxResponse = await response.Content.ReadFromJsonAsync<TaxJarTaxResponse>();

            OrderTax orderTax = tjTaxResponse.Tax.MapToOrderTax();
            orderTax.OrderId = order.Id;
            orderTax.CustomerId = order.CustomerId;
            return orderTax;
        }

        public async Task<TaxRates> GetTaxRates(Location location) {

            // Requirements depend on TaxJar
            TaxJarLocation loc = TaxJarLocation.MapFromLocation(location);
            TaxJarLocationValidator validator = new();
            validator.ValidateAndThrow(loc);

            //Usig Flurl to build the request (but not FlurlUrl because of Json.Net dependency)
            Uri uri = "rates"
                .AppendPathSegment(loc.Zip)
                .SetQueryParam("country", loc.CountryCode)
                .SetQueryParam("city", loc.City)
                .SetQueryParam("state", loc.StateCode)
                .SetQueryParam("street", loc.Street).ToUri();

            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            TaxJarRatesResponse ratesResponse  = await response.Content.ReadFromJsonAsync<TaxJarRatesResponse>();

            return ratesResponse.Rates.MapToTaxRates();
        }
    }
}
