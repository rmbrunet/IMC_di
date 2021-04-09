using FluentValidation;
using System.Text.RegularExpressions;

namespace IMC.Domain.Validators {
    public class LocationValidator : AbstractValidator<Location> {
        public LocationValidator() {
            string usZipPattern = @"^\d{5}(-\d{4})?$";

            // Enforce Zip required & correct format for US
            RuleFor(l => l.Zip)
                .NotEmpty()
                    .WithMessage("Zip code is required.")
                .Matches(usZipPattern)
                .When(l => (l.CountryCode == "US"))
                    .WithMessage("Invalid Zip format for US.");


            // Enforce CountryCode for Zip not conforming to US format
            RuleFor(l => l.CountryCode)
                .NotEmpty()
                    .When(l => (l.Zip != null) && (!Regex.IsMatch(l.Zip, usZipPattern)))
                    .WithMessage("CountryCode is mandatory when zip code not USA ")
                .Length(2)
                    .WithMessage("CountryCode must be two characters long");
        }
    }
}
