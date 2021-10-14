using FluentValidation;
using HotelMgt.Dtos.AmenityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Utilities.Validations.AmenityValidators
{
    public class UpdateAmenityDtoValidator : AbstractValidator<UpdateAmenityDto>
    {
        public UpdateAmenityDtoValidator()
        {
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required")
                .NotNull().WithMessage("Price is required")
                .LessThan(1.0m).WithMessage("Enter a valid Price value");
            RuleFor(x => x.Discount)
                .NotEmpty().WithMessage("Discount is required")
                .NotNull().WithMessage("Discount is required")
                .LessThan(1.0m).WithMessage("Enter a valid discount value");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Amenity name is required")
                .NotNull().WithMessage("Amenity name is required");
        }
    }
}
