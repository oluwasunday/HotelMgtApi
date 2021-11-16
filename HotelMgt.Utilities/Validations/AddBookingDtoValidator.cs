using FluentValidation;
using HotelMgt.Dtos.BookingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Utilities.Validations
{
    public class AddBookingDtoValidator : AbstractValidator<AddBookingDto>
    {
        public AddBookingDtoValidator()
        {
            RuleFor(x => x.CheckIn)
                .NotEmpty()
                .NotNull()
                .WithMessage("Checking is required");
            RuleFor(x => x.ServiceName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Service name is required");
            RuleFor(x => x.NoOfPeople)
                .NotEmpty()
                .NotNull()
                .WithMessage("Specify number of people");
            RuleFor(x => x.CheckOut)
                .NotEmpty().WithMessage("Checkout field can not be empty")
                .NotNull().WithMessage("Checkout field can not be null");
            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("Date created can not be empty")
                .NotNull().WithMessage("Date created can not be null");
        }
    }
}
