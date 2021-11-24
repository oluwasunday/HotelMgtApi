using FluentValidation;
using HotelMgt.Dtos.AmenityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Utilities.Validations.AmenityValidators
{
    public class AddAmenityDtoValidator : AbstractValidator<AddAmenityDto>
    {
        public AddAmenityDtoValidator()
        {
            RuleFor(x => x.RoomTypeId)
                .NotEmpty().WithMessage("Field is required")
                .NotNull().WithMessage("Field is required");
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required")
                .NotEmpty().WithMessage("Name is required");
            
        }
    }
}
