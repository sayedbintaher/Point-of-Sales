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

            //Validation for Items
            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("Transaction items are required.");

            //Validation for FirstName
            RuleFor(x => x.FullName)
                .NotEmpty()
                .Length(3, 300)
                .WithMessage("Product name is required and must be between 3 to 300 characters long.")
                .Must(value => !value.StartsWith(" ") && !value.EndsWith(" "))
                .WithMessage("{PropertyName} cannot have leading or trailing spaces.");

            //Validation for Single Item
            RuleForEach(x => x.Items)
                .SetValidator(new TransactionItemCreateVMValidator());
        }
    }

    public class TransactionItemCreateVMValidator : AbstractValidator<TransactionItemCreateVM>
    {
        public TransactionItemCreateVMValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
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

            //Validation for Items
            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("Transaction items are required.");

            //Validation for Single Item
            RuleForEach(x => x.Items)
                .SetValidator(new TransactionItemCreateVMValidator());
        }
    }
}
