using AutoMapper;
using HotelBookingApi.DTOs;
using HotelBookingApi.Models;

namespace HotelBookingApi.Mappings
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();
        }
    }
}
