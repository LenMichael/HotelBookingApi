using HotelBookingApi.Dtos;
using HotelBookingApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace HotelBookingApi.Extensions
{
    public static class HotelBookingExtensions
    {
        public static BookingDto ToDto(this Booking entity)
        {
            return new BookingDto
            {
                Id = entity.Id,
                CustomerName = entity.CustomerName,
                CheckInDate = entity.CheckInDate,
                CheckOutDate = entity.CheckOutDate,
                RoomId = entity.RoomId
            };
        }

        public static Booking ToEntity(this CreateBookingDto dto)
        {
            return new Booking
            {
                CustomerName = dto.CustomerName,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                RoomId = dto.RoomId
            };
        }

        public static void UpdateEntity(this Booking entity, UpdateBookingDto dto)
        {
            entity.RoomId = dto.RoomId;
            entity.CheckInDate = dto.CheckInDate;
            entity.CheckOutDate = dto.CheckOutDate;
        }
    }
}
