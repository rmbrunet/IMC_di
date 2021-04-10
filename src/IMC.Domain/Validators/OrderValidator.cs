using FluentValidation;

namespace IMC.Domain.Validators {
    public class OrderValidator : AbstractValidator<Order> {
        public OrderValidator() {
            RuleFor(o => o.CustomerId).NotEmpty();
            RuleFor(o => o.TaxCalculatorId).NotEmpty(); 
            RuleFor(o => o.AddressFrom).NotNull().SetValidator(new LocationValidator());
            RuleFor(o => o.AddressTo).NotNull().SetValidator(new LocationValidator());
            RuleFor(o => o.LineItems)
                .NotNull()
                .DependentRules(() => RuleFor(x => x.LineItems)
                    .Must(li => li.Count > 0)
                    .WithMessage("Orders must contain at least one LineItem"));
            RuleForEach(o => o.LineItems).SetValidator(new LineItemValidator());
        }
    }
}
