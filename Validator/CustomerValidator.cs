using FluentValidation;
using PosAPI.ViewModels;

namespace PosAPI.Validator
{
    public class CustomerCreateValidator : AbstractValidator<CustomerCreateVM>
    {
        public CustomerCreateValidator()
        {
            //Validation for FirstName
            RuleFor(x => x.FullName)
             .NotEmpty()
             .Length(3, 300)
             .WithMessage("Product name is required and must be between 3 to 300 characters long.")
             .Must(value => !value.StartsWith(" ") && !value.EndsWith(" "))
             .WithMessage("{PropertyName} cannot have leading or trailing spaces.");


            //Validation for Email
            RuleFor(x => x.Email)
               .EmailAddress()
               .WithMessage("A valid {PropertyName} is required!")
               .Length(3, 100)
               .Must(value => string.IsNullOrWhiteSpace(value) || (value.Length >= 3 && value.Length <= 100))
               .WithMessage("{PropertyName} must be between 3 and 100 characters long.")
               .Must(value => !value.Contains("||"))
               .WithMessage("{PropertyName} cannot contain the characters '||'")
               .Must(value => !value.Contains("&&"))
               .WithMessage("{PropertyName} cannot contain the characters '&&'")
               .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]{3}$")
               .WithMessage("Email must have a valid domain.")
               .Must(value => !value.Contains(" "))
               .WithMessage("{PropertyName} cannot contain spaces.")
               .Must(value => !value.StartsWith(" ") && !value.EndsWith(" "))
               .WithMessage("{PropertyName} cannot have leading or trailing spaces.")
               .Must(value => value == value.ToLower())
               .WithMessage("{PropertyName} can only contain lowercase letters.");

            //Validation for PhoneNo
            RuleFor(x => x.PhoneNo)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Length(11, 12).WithMessage("{PropertyName} must be between 11 and 12 characters long.")
                .Matches(@"^[0-9]+$").WithMessage("{PropertyName} can only contain numeric digits.")
                .Must(value => !value.Contains(" "))
                .WithMessage("{PropertyName} cannot contain spaces.")
                .Must(value => !value.StartsWith(" ") && !value.EndsWith(" "))
                .WithMessage("{PropertyName} cannot have leading or trailing spaces.");

            //Validation for Address
            RuleFor(x => x.Address)
               .Length(3, 500)
               .Must(value => string.IsNullOrWhiteSpace(value) || (value.Length >= 3 && value.Length <= 500))
               .WithMessage("{PropertyName} must be between 3 to 500 characters long.")
               .Must(value => !value.Contains("||")).WithMessage("{PropertyName} cannot contain the characters '||'")
               .Must(value => !value.Contains("&&")).WithMessage("{PropertyName} cannot contain the characters '&&'");
        }
    }
    public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateVM>
    {
        public CustomerUpdateValidator()
        {

            //Validation for Id
            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");

            //Validation for FirstName
            RuleFor(x => x.FullName)
             .NotEmpty()
             .Length(3, 300)
             .WithMessage("Product name is required and must be between 3 to 300 characters long.")
             .Must(value => !value.StartsWith(" ") && !value.EndsWith(" "))
             .WithMessage("{PropertyName} cannot have leading or trailing spaces.");

            //Validation for Email
            RuleFor(x => x.Email)
               .EmailAddress()
               .WithMessage("A valid {PropertyName} is required!")
               .Length(3, 100)
               .Must(value => string.IsNullOrWhiteSpace(value) || (value.Length >= 3 && value.Length <= 100))
               .WithMessage("{PropertyName} must be between 3 and 100 characters long.")
               .Must(value => !value.Contains("||"))
               .WithMessage("{PropertyName} cannot contain the characters '||'")
               .Must(value => !value.Contains("&&"))
               .WithMessage("{PropertyName} cannot contain the characters '&&'")
               .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]{3}$")
               .WithMessage("Email must have a valid domain.")
               .Must(value => !value.Contains(" "))
               .WithMessage("{PropertyName} cannot contain spaces.")
               .Must(value => !value.StartsWith(" ") && !value.EndsWith(" "))
               .WithMessage("{PropertyName} cannot have leading or trailing spaces.")
               .Must(value => value == value.ToLower())
               .WithMessage("{PropertyName} can only contain lowercase letters.");

            //Validation for PhoneNo
            RuleFor(x => x.PhoneNo)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Length(11, 12).WithMessage("{PropertyName} must be between 11 and 12 characters long.")
                .Matches(@"^[0-9]+$").WithMessage("{PropertyName} can only contain numeric digits.")
                .Must(value => !value.Contains(" "))
                .WithMessage("{PropertyName} cannot contain spaces.")
                .Must(value => !value.StartsWith(" ") && !value.EndsWith(" "))
                .WithMessage("{PropertyName} cannot have leading or trailing spaces.");

            //Validation for Address
            RuleFor(x => x.Address)
               .Length(3, 500)
               .Must(value => string.IsNullOrWhiteSpace(value) || (value.Length >= 3 && value.Length <= 500))
               .WithMessage("{PropertyName} must be between 3 to 500 characters long.")
               .Must(value => !value.Contains("||")).WithMessage("{PropertyName} cannot contain the characters '||'")
               .Must(value => !value.Contains("&&")).WithMessage("{PropertyName} cannot contain the characters '&&'");
        }
    }
}
