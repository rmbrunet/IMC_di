using FluentValidation;

namespace IMC.Domain.Validators {
    public class LocationValidator : AbstractValidator<Location> {
        public LocationValidator() {
            RuleFor(l => l.CountryCode)
                .NotNull().WithMessage("CountryCode is mandatory")
                .DependentRules(() => RuleFor(l => l.CountryCode).Length(2).WithMessage("CountryCode must be two characters long")); ;
        }
    }
}
