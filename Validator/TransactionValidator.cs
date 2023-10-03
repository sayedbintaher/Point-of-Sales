using FluentValidation;
using PosAPI.ViewModels;

namespace PosAPI.Validator
{
    public class TransactionCreateValidator : AbstractValidator<TransactionCreateVM>
    {
        public TransactionCreateValidator() 
        {
            //Validation for Price
            RuleFor(x => x.PaymentMethodId)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");

            //Validation for Stock
            RuleFor(x => x.TotalAmount)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        }
    }

    public class TransactionUpdateValidator : AbstractValidator<TransactionUpdateVM>
    {
        public TransactionUpdateValidator()
        {
            //Validation for Id
            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");

            //Validation for Price
            RuleFor(x => x.PaymentMethodId)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");

            //Validation for Stock
            RuleFor(x => x.TotalAmount)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        }
    }
}
