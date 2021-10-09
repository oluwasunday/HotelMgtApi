using AutoMapper;
using HotelMgt.Dtos;
using HotelMgt.Dtos.BookingDtos;
using HotelMgt.Dtos.CustomerDtos;
using HotelMgt.Dtos.RoomDtos;
using HotelMgt.Models;

namespace HotelMgt.Core.Utilities
{
    public class ModelMaps : Profile
    {
        public ModelMaps()
        {
            CreateMap<AppUser, AddUserDto>().ReverseMap();
            CreateMap<AddUserDto, AppUser>().ReverseMap();
            CreateMap<AppUser, AddUserResponseDto>()
                .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

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

            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Room, AddRoomDto>().ReverseMap();
            CreateMap<AddRoomResponseDto, Room>().ReverseMap();
        }
    }
}
