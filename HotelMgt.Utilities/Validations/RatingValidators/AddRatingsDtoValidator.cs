using FluentValidation;
using HotelMgt.Dtos.BookingDtos;
using HotelMgt.Dtos.RatingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Utilities.Validations.RatingValidators
{
    public class AddRatingsDtoValidator : AbstractValidator<AddRatingsDto>
    {
        public AddRatingsDtoValidator()
        {
            RuleFor(x => x.Ratings)
                .NotNull().WithMessage("Rate value is required")
                .NotEmpty().WithMessage("Rate value is required")
                .InclusiveBetween(1, 6).WithMessage("Invalid rate value, must be between 1 and 5");
            RuleFor(x => x.Comment)
                .NotNull().WithMessage("Comment is required")
                .NotEmpty().WithMessage("Comment is required");
        }
    }
}
