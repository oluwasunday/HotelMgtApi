using FluentValidation;
using hotel_booking_core.Services;
using HotelMgt.Core;
using HotelMgt.Core.interfaces;
using HotelMgt.Core.Repositories.implementations;
using HotelMgt.Core.Repositories.interfaces;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Core.Services.implementations;
using HotelMgt.Core.UnitOfWork.abstractions;
using HotelMgt.Core.UnitOfWork.implementations;
using HotelMgt.Dtos.AmenityDtos;
using HotelMgt.Dtos.AuthenticationDto;
using HotelMgt.Dtos.BookingDtos;
using HotelMgt.Dtos.CustomerDtos;
using HotelMgt.Dtos.RatingDtos;
using HotelMgt.Dtos.ReviewDtos;
using HotelMgt.Utilities;
using HotelMgt.Utilities.Settings;
using HotelMgt.Utilities.Validations;
using HotelMgt.Utilities.Validations.AmenityValidators;
using HotelMgt.Utilities.Validations.AuthenticationValidators;
using HotelMgt.Utilities.Validations.RatingValidators;
using HotelMgt.Utilities.Validations.ReviewValidations;
using Microsoft.Extensions.DependencyInjection;

namespace HotelMgt.API.Extensions
{
    public static class ServicesExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {

            // Add Repository Injections Here
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository,  UserRepository>();
            services.AddScoped<IBookingsRepository, BookingsRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            // Add Model Services Injection Here
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IBookingService,  BookingService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IAmenityService, AmenityService>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<IPaymentService, PaymentService>();

            // Add Fluent Validator Injections Here
            services.AddTransient<IValidator<AddCustomerDto>, AddCustomerDtoValidator>();
            services.AddTransient<IValidator<UpdateRatingDto>, UpdateRatingValidatorDto>();
            services.AddTransient<IValidator<AddBookingDto>, AddBookingDtoValidator>();
            services.AddTransient<IValidator<AddReviewDto>, AddReviewDtoValidator>();
            services.AddTransient<IValidator<UpdateReviewDto>, UpdateReviewDtoValidator>();
            services.AddTransient<IValidator<LoginDto>, LoginValidatorDto>();
            services.AddTransient<IValidator<RegisterDto>, RegisterDtoValidator>();
            services.AddTransient<IValidator<ResetPasswordDto>, ResetPasswordValidatorDto>();
            services.AddTransient<IValidator<AddAmenityDto>, AddAmenityDtoValidator>();
            services.AddTransient<IValidator<UpdateAmenityDto>, UpdateAmenityDtoValidator>();
            services.AddTransient<IValidator<AddRatingsDto>, AddRatingsDtoValidator>();

            // Authentication
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();

        }
    }
}
