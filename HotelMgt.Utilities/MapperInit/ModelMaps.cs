using AutoMapper;
using hotel_booking_models;
using HotelMgt.Dtos;
using HotelMgt.Dtos.AmenityDtos;
using HotelMgt.Dtos.AuthenticationDto;
using HotelMgt.Dtos.BookingDtos;
using HotelMgt.Dtos.CustomerDtos;
using HotelMgt.Dtos.GalleryDtos;
using HotelMgt.Dtos.RatingDtos;
using HotelMgt.Dtos.ReviewDtos;
using HotelMgt.Dtos.RoomDtos;
using HotelMgt.Dtos.RoomTypeDtos;
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
                .ForMember(dest => dest.PaymentReference, opt => opt.MapFrom(src => src.Payment.TransactionReference))
                .ForMember(x => x.RoomType, y => y.MapFrom(src => src.Room.Roomtype.Name))
                .ForMember(x => x.RoomNo, y => y.MapFrom(src => src.Room.RoomNo))
                .ForMember(x => x.Price, y => y.MapFrom(src => src.Room.Roomtype.Price))
                .ForMember(x => x.BookingStatus, y => y.MapFrom(src => src.BookingStatus));
            CreateMap<Booking, BookingResponseDto>()
                .ForMember(x => x.RoomNo, y => y.MapFrom(z => z.Room.RoomNo))
                .ForMember(x => x.Amount, y => y.MapFrom(z => z.Room.Roomtype.Price));

            // room mappings
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Room, AddRoomDto>().ReverseMap();
            CreateMap<AddRoomResponseDto, Room>().ReverseMap();

            // rating mappings
            CreateMap<AddRatingResponseDto, Rating>().ReverseMap();
            CreateMap<AddRatingsDto, Rating>().ReverseMap();

            // amenity mappings
            CreateMap<AddAmenityDto, Amenity>().ReverseMap();
            CreateMap<AddAmenityResponseDto, Amenity>().ReverseMap();

            // roomtype mappings
            CreateMap<AddRoomTypeDto, RoomType>().ReverseMap();
            CreateMap<RoomTypeResponseDto, RoomType>().ReverseMap();
            CreateMap<UpdateRoomTypeDto, RoomType>().ReverseMap();

            // gallery mappings
            CreateMap<AddGalleryDto, Gallery>().ReverseMap();
            CreateMap<Gallery, AddGalleryResponseDto>().ReverseMap();
        }
    }
}
