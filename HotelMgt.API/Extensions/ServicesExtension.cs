using FluentValidation;
using hotel_booking_core.Services;
using HotelMgt.Core;
using HotelMgt.Core.interfaces;
using HotelMgt.Core.Services.abstractions;
using HotelMgt.Core.Services.implementations;
using HotelMgt.Core.UnitOfWork.abstractions;
using HotelMgt.Core.UnitOfWork.implementations;
using HotelMgt.Dtos.CustomerDtos;
using HotelMgt.Utilities;
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

            // Add Fluent Validator Injections Here
            services.AddTransient<IValidator<AddCustomerDto>, AddCustomerDtoValidator>();

            // Authentication
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        }
    }
}
