using IMC.Domain;
using System.Threading.Tasks;

namespace IMC.Application.Interfaces {
    public interface ITaxCalculator {
        public string Id { get; }
        Task<TaxRates> GetTaxRates(Location location);
        Task<OrderTax> GetSalesTax(Order order);
    }
}
