using IMC.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMC.Application
{
    public class TaxCalculatorProvider : ITaxCalculatorProvider {
        private readonly IEnumerable<ITaxCalculator> _taxCalculators;
        public TaxCalculatorProvider(IEnumerable<ITaxCalculator> taxCalculators) {
            _taxCalculators = taxCalculators;
        }
        public ITaxCalculator GetTaxCalculator(string id) {
            return _taxCalculators.First(tc => tc.Id == id);
        }
    }
}
