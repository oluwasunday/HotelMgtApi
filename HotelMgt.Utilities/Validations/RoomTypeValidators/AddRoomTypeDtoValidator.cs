using FluentValidation;
using HotelMgt.Dtos.RoomTypeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Utilities.Validations.RoomTypeValidators
{
    public class AddRoomTypeDtoValidator : AbstractValidator<AddRoomTypeDto>
    {
        public AddRoomTypeDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Room type name is required");
        }
    }
}
