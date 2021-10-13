using FluentValidation;
using HotelMgt.Dtos.RatingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Utilities.Validations
{
    public class UpdateRatingValidatorDto : AbstractValidator<UpdateRatingDto>
    {
        public UpdateRatingValidatorDto()
        {
            RuleFor(x => x.Ratings)
                .NotEmpty().WithMessage("Rating can't be empty")
                .GreaterThanOrEqualTo(1).WithMessage("Rating must be between 1 - 5")
                .LessThanOrEqualTo(5).WithMessage("Rating must be between 1 - 5");
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Can't be empty")
                .NotNull().WithMessage("Can't be null");
        }
    }
}
