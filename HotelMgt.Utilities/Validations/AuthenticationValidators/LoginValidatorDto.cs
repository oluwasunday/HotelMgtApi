using FluentValidation;
using HotelMgt.Dtos.AuthenticationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Utilities.Validations.AuthenticationValidators
{
    public class LoginValidatorDto : AbstractValidator<LoginDto>
    {
        public LoginValidatorDto()
        {
            RuleFor(user => user.Password).Password();

            RuleFor(user => user.Email).EmailAddress();
        }
    }
}
