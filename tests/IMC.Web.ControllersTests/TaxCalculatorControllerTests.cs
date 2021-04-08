using FluentValidation;
using IMC.Application.Interfaces;
using IMC.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Web.Controllers;
using Xunit;

namespace IMC.Web.ControllersTests {
    public class TaxCalculatorControllerTests {
        [Fact]
        async Task Controller_returns_the_right_TaxRates() {

            // Arrange
            var taxCalculatorMock = new Mock<ITaxCalculator>();
            var loggerMock = new Mock<ILogger<TaxCalculatorController>>();

            TaxRates taxRates = new() {
                Zip = "33029",
                CityRate = 0m,
                City = "Hollywood",
                CountryRate = 0m,
                Country = "US",
                State = "FL",
                StateRate = 0.06m,
                County = "BROWARD",
                CountyRate = 0.01m,
                CombinedDistrictRate = 0m,
                CombinedRate = 0.07m,
                FreightTaxable = false
            };

            taxCalculatorMock.Setup(tx => tx.GetTaxRates(It.IsAny<Location>())).ReturnsAsync(taxRates);

            var controller = new TaxCalculatorController(taxCalculatorMock.Object, loggerMock.Object);

            // Act
            ActionResult<TaxRates> response = await controller.GetTaxRates("33029", null);

            // Assert
            OkObjectResult result = response.Result as OkObjectResult;
            Assert.NotNull(result);
            TaxRates tr = Assert.IsAssignableFrom<TaxRates>(result.Value);
            Assert.IsType<ActionResult<TaxRates>>(response);
            Assert.Equal(taxRates.CombinedRate, tr.CombinedRate);
            Assert.Equal(taxRates.City, tr.City);
            Assert.Equal(taxRates.CityRate, tr.CityRate);
            Assert.Equal(taxRates.CombinedDistrictRate, tr.CombinedDistrictRate);
            Assert.Equal(taxRates.Country, tr.Country);
            Assert.Equal(taxRates.CountryRate, tr.CountryRate);
            Assert.Equal(taxRates.County, tr.County);
            Assert.Equal(taxRates.CountyRate, tr.CountyRate);
            Assert.Equal(taxRates.FreightTaxable, tr.FreightTaxable);
            Assert.Equal(taxRates.State, tr.State);
            Assert.Equal(taxRates.StateRate, tr.StateRate);
            Assert.Equal(taxRates.Zip, tr.Zip);

        }

        [Fact]
        async Task Controller_returns_the_right_SalesTaxForOrder() {
            
            // Arrange
            
            var taxCalculatorMock = new Mock<ITaxCalculator>();
            var loggerMock = new Mock<ILogger<TaxCalculatorController>>();

            OrderTax orderTax = new() {
                OrderId = "1",
                CustomerId = "1",
                AmountToCollect = 1.50m,
                FreightTaxable = false,
                OrderTotalAmount = 16.5m,
                Shipping = 1.5m,
                TaxableAmount = 15.0m
            };

            taxCalculatorMock.Setup(tx => tx.GetSalesTax(It.IsAny<Order>())).ReturnsAsync(orderTax);

            var controller = new TaxCalculatorController(taxCalculatorMock.Object, loggerMock.Object);

            // Minimal Order To Pass Validation
            var order = new Order {
                Id = "1",
                CustomerId = "1",
                Shipping = 1.5m,
                AddressFrom = new() {
                    CountryCode = "US",
                    StateCode = "FL",
                    Zip = "33029"
                },
                AddressTo = new() {
                    CountryCode = "US",
                    StateCode = "FL",
                    Zip = "33174"
                },
                LineItems = new() {
                    new() {
                        Id = "abc",
                        ProductTaxCode = "1111",
                        Quantity = 10,
                        UnitPrice = 1.5m,
                        Discount = 0
                    }
                }
            };

            // Act
            ActionResult<OrderTax> response = await controller.GetTaxes(order);

            // Assert
            OkObjectResult result = response.Result as OkObjectResult;
            Assert.NotNull(result);
            OrderTax tax = Assert.IsAssignableFrom<OrderTax>(result.Value);
            Assert.Equal(orderTax.OrderId, tax.OrderId);
            Assert.Equal(orderTax.CustomerId, tax.CustomerId);
            Assert.Equal(orderTax.AmountToCollect, tax.AmountToCollect);
            Assert.Equal(orderTax.FreightTaxable, tax.FreightTaxable);
            Assert.Equal(orderTax.OrderTotalAmount, tax.OrderTotalAmount);
            Assert.Equal(orderTax.Shipping, tax.Shipping);
            Assert.Equal(orderTax.TaxableAmount, tax.TaxableAmount);

        }

        [Fact]
        async Task Controller_returns_BadRequest_when_LineItems_Empty() {
            // Arrange

            var taxCalculatorMock = new Mock<ITaxCalculator>();
            var loggerMock = new Mock<ILogger<TaxCalculatorController>>();

            OrderTax orderTax = new() {
                OrderId = "1",
                CustomerId = "1",
                AmountToCollect = 1.50m,
                FreightTaxable = false,
                OrderTotalAmount = 16.5m,
                Shipping = 1.5m,
                TaxableAmount = 15.0m
            };

            taxCalculatorMock.Setup(tx => tx.GetSalesTax(It.IsAny<Order>())).ReturnsAsync(orderTax);

            var controller = new TaxCalculatorController(taxCalculatorMock.Object, loggerMock.Object);

            // Order without LineItems 
            var order = new Order {
                Id = "1",
                CustomerId = "1",
                Shipping = 1.5m,
                AddressFrom = new() {
                    CountryCode = "US",
                    StateCode = "FL",
                    Zip = "33029"
                },
                AddressTo = new() {
                    CountryCode = "US",
                    StateCode = "FL",
                    Zip = "33174"
                },
                LineItems = new() {
                }
            };

            // Act
            ActionResult<OrderTax> response = await controller.GetTaxes(order);

            // Assert
            BadRequestObjectResult result = response.Result as BadRequestObjectResult;
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Contains("Orders must contain at least one LineItem", (string)result.Value);
        }
    }
}
