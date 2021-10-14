using AutoMapper;
using HotelMgt.Dtos;
using HotelMgt.Dtos.AuthenticationDto;
using HotelMgt.Dtos.BookingDtos;
using HotelMgt.Dtos.CustomerDtos;
using HotelMgt.Dtos.RatingDtos;
using HotelMgt.Dtos.ReviewDtos;
using HotelMgt.Dtos.RoomDtos;
using HotelMgt.Models;

namespace HotelMgt.Core.Utilities
{
    public class ModelMaps : Profile
    {
        public ModelMaps()
        {
            // user mappings
            CreateMap<AppUser, AddUserDto>().ReverseMap();
            CreateMap<AddUserDto, AppUser>().ReverseMap();
            CreateMap<AppUser, AddUserResponseDto>()
                .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<AppUser, RegisterDto>().ReverseMap();
            CreateMap<AppUser, RegisterResponseDto>()
                .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<RegisterResponseDto, AppUser>().ReverseMap();

            // ForMember used to do custom mapping where fields in both source and destination are not same
            // example: there is no Age and Name properties in AppUser
            // so we do custom mapping

            CreateMap<Customer, AddCustomerDto>().ReverseMap();
            CreateMap<Customer, AddCustomerResponseDto>().ReverseMap();
            CreateMap<AddCustomerResponseDto, Customer>().ReverseMap();

            // booking mappings
            CreateMap<Booking, AddBookingDto>().ReverseMap();
            CreateMap<Booking, AddBookingResponseDto>()
                .ForMember(
                dest => dest.Duration,
                opt => opt.MapFrom(src => $"{src.CheckIn} to {src.CheckOut}"));

            // room mappings
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Room, AddRoomDto>().ReverseMap();
            CreateMap<AddRoomResponseDto, Room>().ReverseMap();

            // rating mappings
            CreateMap<AddRatingResponseDto, Rating>().ReverseMap();
            CreateMap<AddRatingsDto, Rating>().ReverseMap();

            // review mappings
            CreateMap<AddReviewDto, Review>().ReverseMap();
            CreateMap<AddReviewResponseDto, Review>().ReverseMap();
        }
    }
}
