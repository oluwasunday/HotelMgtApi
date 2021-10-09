using FluentValidation;
using HotelMgt.Dtos.AuthenticationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Utilities.Validations.AuthenticationValidators
{
    public class ConfirmEmailDtoValidator : AbstractValidator<ConfirmEmailDto>
    {
        public ConfirmEmailDtoValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email is required");
            RuleFor(x => x.Token)
                .NotEmpty()
                .NotNull()
                .WithMessage("No token generated");
        }
    }
}
