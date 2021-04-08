using FluentValidation;

namespace IMC.Domain.Validators {
    public class LineItemValidator : AbstractValidator<LineItem> {
        public LineItemValidator() {
            RuleFor(li => li.Id).NotEmpty();
            RuleFor(li => li.Quantity).GreaterThan(0).WithMessage("LineItem Quantity must be greater than 0");
        }
    }
}
