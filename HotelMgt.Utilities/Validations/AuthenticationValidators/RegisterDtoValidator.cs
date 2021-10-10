using FluentValidation;
using HotelMgt.Dtos.AuthenticationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Utilities.Validations.AuthenticationValidators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(customer => customer.Email).EmailAddress();

            RuleFor(customer => customer.Password).Password();

            RuleFor(customer => customer.ConfirmPassword).Password(); 

            RuleFor(customer => customer.Age)
                .NotEmpty().WithMessage("Age is required")
                .NotNull().WithMessage("Age is required")
                .GreaterThanOrEqualTo(18).WithMessage("Customer must be greater 18 years and above");
            
            RuleFor(customer => customer.FirstName).HumanName();

            RuleFor(customer => customer.LastName).HumanName();

            RuleFor(customer => customer.Gender)
                .NotNull()
                .NotEmpty()
                .WithMessage("Gender is required");
        }
    }
}
