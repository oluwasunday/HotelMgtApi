using FluentValidation;
using HotelMgt.Dtos.AuthenticationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Utilities.Validations.AuthenticationValidators
{
    public class ResetPasswordValidatorDto : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordValidatorDto()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.NewPassword).Password();
            RuleFor(x => x.ConfirmPassword).Password();
        }
    }
}
