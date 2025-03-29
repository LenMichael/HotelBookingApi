using Microsoft.EntityFrameworkCore;
using HotelBookingApi.Models;



namespace HotelBookingApi.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<HotelBooking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        public virtual DbSet<HotelBooking> GetBookings()
        {
            return Bookings;
        }

    }
}
