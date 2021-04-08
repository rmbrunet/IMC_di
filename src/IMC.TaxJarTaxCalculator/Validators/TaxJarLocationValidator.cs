using IMC.TaxJarTaxCalculator.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace IMC.TaxJarTaxCalculator.Validators {
    public class TaxJarLocationValidator : AbstractValidator<TaxJarLocation> {
        public TaxJarLocationValidator() {
            RuleFor(x => x.Zip).NotEmpty().WithMessage("Zip Code is mandatory.");
            RuleFor(x => x.StateCode).Length(2).WithMessage("State Code should be two characters long.");

            // According to TaxJar documentation CountryCode is mandatory when not USA Location
            // Assuming USA Zip code pattern is particular to USA.
            When(x => x.Zip != null && !Regex.IsMatch(x.Zip, @"\d{5}(-\d{4})?"), () => {
                RuleFor(r => r.CountryCode)
                    .NotEmpty().WithMessage("Country Code is mandatory for Locations different than USA ")
                    .Length(2).WithMessage("Country Code must be two characters long");
            }).Otherwise(() => {
                RuleFor(x => x.CountryCode).Length(2).WithMessage("Country Code must be two characters long");
            });
        }
    }
}
