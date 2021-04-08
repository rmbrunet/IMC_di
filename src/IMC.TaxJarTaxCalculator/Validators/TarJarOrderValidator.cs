using FluentValidation;
using IMC.TaxJarTaxCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMC.TaxJarTaxCalculator.Validators
{
    public class TarJarOrderValidator : AbstractValidator<TaxJarOrder>
    {
        public TarJarOrderValidator() {
            RuleFor(o => o.FromCountry).NotEmpty();
            RuleFor(o => o.ToCountry).NotEmpty().WithMessage("Destination Country Code is required.");

            RuleFor(o => o.ToState)
                .NotEmpty()
                .When(o => (o.ToCountry == "US") || (o.ToCountry == "CA"))
                .WithMessage("State Code is mandatory for US and Canada.");

            RuleFor(o => o.ToZip)
                .NotEmpty()
                .When(o => o.ToCountry == "US")
                .WithMessage("Zip Code is mandatory for US.")
                .DependentRules(() => RuleFor(o => o.ToZip).Matches(@"^\d{5}(-\d{4})?$"));
        }
    }
}
