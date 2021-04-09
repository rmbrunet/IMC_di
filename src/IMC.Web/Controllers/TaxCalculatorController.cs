using FluentValidation;
using IMC.Application.Interfaces;
using IMC.Domain;
using IMC.Domain.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Web.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase {
        private readonly ITaxCalculator _taxCalculator;
        private readonly ILogger<TaxCalculatorController> _logger;

        public TaxCalculatorController(ITaxCalculator taxCalculator, ILogger<TaxCalculatorController> logger) {
            _taxCalculator = taxCalculator;
            _logger = logger;
        }

        /// <summary>
        /// GET operation to obtain tax rates for a given location.
        /// </summary>
        /// <param name="zipCode"></param>
        /// <param name="countryCode"></param>
        /// <remarks>
        /// Sample return value
        /// 
        ///     {
        ///         "location": {
        ///             "id": null,
        ///             "countryCode": "US",
        ///             "zip": "33029-6009",
        ///             "stateCode": "FL",
        ///             "city": null,
        ///             "street": null,
        ///             "county": "BROWARD",
        ///             "country": null,
        ///             "state": null
        ///         },
        ///         "cityRate": 0.0,
        ///         "combinedDistrictRate": 0.0,
        ///         "combinedRate": 0.07,
        ///         "countryRate": 0.0,
        ///         "countyRate": 0.01,
        ///         "freightTaxable": false,
        ///         "stateRate": 0.06
        ///     }
        ///     
        /// </remarks>
        /// <returns></returns>
        [HttpGet("rates/{zipCode}", Name = nameof(GetTaxRates))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TaxRates), 200)]
        [ProducesResponseType(typeof(string), 400)]

        public async Task<ActionResult<TaxRates>> GetTaxRates([FromRoute] string zipCode, [FromQuery] string countryCode) {
            try {
                Location location = new() { CountryCode = countryCode, Zip = zipCode };
                LocationValidator validator = new();
                validator.ValidateAndThrow(location);

                TaxRates taxRates = await _taxCalculator.GetTaxRates(location);

                return Ok(taxRates);
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

    /// <summary>
    /// POST Operation to calculate the sales tax of an Order
    /// </summary>
    /// <param name="order"></param>
    /// <remarks>
    /// Sample request
    /// 
    ///         {
    ///           "id": "1",
    ///           "customerId": "1",
    ///           "addressTo": {
    ///             "countryCode": "US",
    ///             "zip": "90002",
    ///             "stateCode": "CA",
    ///             "city": "Los Angeles",
    ///             "street": "1335 E 103rd St"
    ///           },
    ///           "addressFrom": {
    ///             "countryCode": "US",
    ///             "zip": "92093",
    ///             "stateCode": "CA",
    ///             "city": "La Jolla",
    ///             "street": "9500 Gilman Drive"
    ///           },
    ///           "shipping": 1.5,
    ///           "lineItems": [
    ///             {
    ///               "id": "abc",
    ///               "quantity": 1,
    ///               "productTaxCode": "20010",
    ///               "unitPrice": 15,
    ///               "discount": 0
    ///             }
    ///           ]
    ///         }
    ///        
    /// Sample response
    /// 
    ///         {
    ///             "orderId": "1",
    ///             "customerId": "1",
    ///             "amountToCollect": 1.43,
    ///             "freightTaxable": false,
    ///             "orderTotalAmount": 16.5,
    ///             "rate": 0.095,
    ///             "shipping": 1.5,
    ///             "taxableAmount": 15.0
    ///         }
    ///         
    /// </remarks>
    /// <returns></returns>
    [HttpPost("taxes", Name = nameof(GetTaxes))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderTax), 200)]

        public async Task<ActionResult<OrderTax>> GetTaxes([FromBody]Order order) {
            try {
                // TODO: Integrate with ASPNET Core
                OrderValidator validator = new();
                validator.ValidateAndThrow(order);

                OrderTax tax = await _taxCalculator.GetSalesTax(order);
                return Ok(tax);
            }
            catch (Exception vex) {
                return BadRequest(vex.Message);
            }
        }
    }
}
