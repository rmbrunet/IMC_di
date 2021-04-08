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

        [HttpGet("rates/{zipCode}", Name = nameof(GetTaxRates))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TaxRates), 200)]
        [ProducesResponseType(typeof(string), 400)]

        public async Task<ActionResult<TaxRates>> GetTaxRates([FromRoute] string zipCode, [FromQuery] string countryCode) {
            try {
                Location location = new() { CountryCode = countryCode, Zip = zipCode };
                
                TaxRates taxRates = await _taxCalculator.GetTaxRates(location);

                return Ok(taxRates);
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

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
