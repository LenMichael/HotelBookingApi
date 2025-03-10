using Microsoft.EntityFrameworkCore;
using HotelBookingApi.Models;



namespace HotelBookingApi.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<HotelBooking> Bokkings { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

    }
}
