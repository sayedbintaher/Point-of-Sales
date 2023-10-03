using FluentValidation;
using PosAPI.ViewModels;

namespace PosAPI.Validator
{
    public class ProductCreateValidator : AbstractValidator<ProductCreateVM>
    {
        public ProductCreateValidator()
        {
            //Validation for Name
            RuleFor(x => x.Name)
             .NotEmpty()
             .Length(3, 100)
             .WithMessage("Product name is required and must be between 3 to 100 characters long.")
             .Must(value => !value.StartsWith(" ") && !value.EndsWith(" "))
             .WithMessage("{PropertyName} cannot have leading or trailing spaces.");

            //Validation for Description
            RuleFor(x => x.Description)
             .Length(3, 500)
             .Must(value => string.IsNullOrWhiteSpace(value) || (value.Length >= 3 && value.Length <= 500))
             .WithMessage("{PropertyName} must be between 3 to 500 characters long.")
             .Must(value => !value.StartsWith(" ") && !value.EndsWith(" "))
             .WithMessage("{PropertyName} cannot have leading or trailing spaces.");

            //Validation for Price
            RuleFor(x => x.Price)
             .GreaterThan(0)
             .WithMessage("{PropertyName} must be greater than zero.");

            //Validation for Stock
            RuleFor(x => x.Stock)
             .GreaterThan(0)
             .WithMessage("{PropertyName} must be greater than zero.");
        }
    }
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateVM>
    {
        public ProductUpdateValidator()
        {
            //Validation for Id
            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
            //.Equal(id).WithMessage("{PropertyName} must be same as the provided id.");

            //Validation for Name
            RuleFor(x => x.Name)
             .NotEmpty()
             .Length(3, 100)
             .WithMessage("Product name is required and must be between 3 to 100 characters long.")
             .Must(value => !value.StartsWith(" ") && !value.EndsWith(" "))
             .WithMessage("{PropertyName} cannot have leading or trailing spaces.");

            //Validation for Description
            RuleFor(x => x.Description)
             .Length(3, 500)
             .Must(value => string.IsNullOrWhiteSpace(value) || (value.Length >= 3 && value.Length <= 500))
             .WithMessage("{PropertyName} must be between 3 to 500 characters long.")
             .Must(value => !value.StartsWith(" ") && !value.EndsWith(" "))
             .WithMessage("{PropertyName} cannot have leading or trailing spaces.");

            //Validation for Price
            RuleFor(x => x.Price)
             .GreaterThan(0)
             .WithMessage("{PropertyName} must be greater than zero.");

            //Validation for Stock
            RuleFor(x => x.Stock)
             .GreaterThan(0)
             .WithMessage("{PropertyName} must be greater than zero.");
        }
    }
}
