﻿using FluentValidation;
using HotelMgt.Dtos.CustomerDtos;
using System;

namespace HotelMgt.Utilities
{
    public class AddCustomerDtoValidator : AbstractValidator<AddCustomerDto>
    {
        public AddCustomerDtoValidator()
        {
            RuleFor(x => x.State)
                .NotEmpty().WithMessage("state is required")
                .NotNull().WithMessage("state is required");

            RuleFor(x => x.Address)
                .NotEmpty()
                .NotNull()
                .WithMessage("address is required");
        }
    }
}
