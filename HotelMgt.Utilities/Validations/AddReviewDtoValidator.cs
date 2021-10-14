using FluentValidation;
using HotelMgt.Dtos.RatingDtos;
using HotelMgt.Dtos.ReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Utilities.Validations
{
    public class AddReviewDtoValidator : AbstractValidator<AddReviewDto>
    {
        public AddReviewDtoValidator()
        {
            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Comment can't be empty")
                .NotNull().WithMessage("Comment can't be empty");
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("CustomerId is required")
                .NotNull().WithMessage("CustomerId is required");
        }
    }
}
