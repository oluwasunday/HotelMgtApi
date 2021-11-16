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
            RuleFor(x => x.Price)
                .LessThanOrEqualTo(0.0m).WithMessage("Enter valid figure")
                .NotEmpty().WithMessage("Price is required")
                .NotNull().WithMessage("Price is required");
            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0.0m).WithMessage("Enter valid figure")
                .NotEmpty().WithMessage("Discount is required")
                .NotNull().WithMessage("Discount is required");
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required")
                .NotEmpty().WithMessage("Name is required");
            
        }
    }
}
