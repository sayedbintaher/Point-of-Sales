using FluentValidation;
using PosAPI.ViewModels;

namespace PosAPI.Validator
{
    public class InventoryCreateValidator : AbstractValidator<InventoryCreateVM>
    {
        public InventoryCreateValidator() 
        {
            //Validation for Price
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");

            //Validation for Stock
            RuleFor(x => x.StockQuantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        }
    }
    public class InventoryUpdateValidator : AbstractValidator<InventoryUpdateVM>
    {
        public InventoryUpdateValidator()
        {
            //Validation for Id
            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");

            //Validation for Price
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");

            //Validation for Stock
            RuleFor(x => x.StockQuantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        }
    }
}
