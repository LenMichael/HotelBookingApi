using Microsoft.EntityFrameworkCore;
using HotelBookingApi.Models;

namespace HotelBookingApi.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        public virtual DbSet<Booking> GetBookings()
        {
            return Bookings;
        }

        //Initial Migration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = "Admin" },
                new User { Id = 2, Username = "itsupport", PasswordHash = BCrypt.Net.BCrypt.HashPassword("itsupport123"), Role = "IT" },
                new User { Id = 3, Username = "employee", PasswordHash = BCrypt.Net.BCrypt.HashPassword("employee123"), Role = "Employee" }
            );


            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "Grand Hotel", Address = "123 Main Street", PhoneNumber = "123-456-7890", Email = "info@grandhotel.com" },
                new Hotel { Id = 2, Name = "Ocean View", Address = "456 Beach Road", PhoneNumber = "987-654-3210", Email = "contact@oceanview.com" }
            );

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, RoomNumber = 101, Type = "Single", Price = 50.00m, HotelId = 1 },
                new Room { Id = 2, RoomNumber = 102, Type = "Double", Price = 75.00m, HotelId = 1 },
                new Room { Id = 3, RoomNumber = 201, Type = "Suite", Price = 120.00m, HotelId = 2 }
            );

            //Rooms -> Bookings
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany()
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Users -> Bookings
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 1,
                    CustomerName = "John Doe",
                    RoomId = 1,
                    UserId = 3,
                    CheckInDate = DateTime.SpecifyKind(new DateTime(2025, 4, 20), DateTimeKind.Utc),
                    CheckOutDate = DateTime.SpecifyKind(new DateTime(2025, 4, 25), DateTimeKind.Utc),
                    Status = "Confirmed"
                },
                new Booking
                {
                    Id = 2,
                    CustomerName = "Jane Smith",
                    RoomId = 2,
                    UserId = 3,
                    CheckInDate = DateTime.SpecifyKind(new DateTime(2025, 5, 1), DateTimeKind.Utc),
                    CheckOutDate = DateTime.SpecifyKind(new DateTime(2025, 5, 5), DateTimeKind.Utc),
                    Status = "Cancelled"
                }
            );

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    Id = 1,
                    BookingId = 1,
                    Amount = 250.00m,
                    PaymentDate = DateTime.SpecifyKind(new DateTime(2025, 4, 15), DateTimeKind.Utc),
                    PaymentMethod = "Credit Card"
                },
                new Payment
                {
                    Id = 2,
                    BookingId = 2,
                    Amount = 375.00m,
                    PaymentDate = DateTime.SpecifyKind(new DateTime(2025, 4, 20), DateTimeKind.Utc),
                    PaymentMethod = "Cash"
                }
            );

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Hotel)
                .WithMany()
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, UserId = 1, HotelId = 1, Position = "Manager" },
                new Employee { Id = 2, UserId = 2, HotelId = 2, Position = "Receptionist" }
            );

            // Logs -> Users
            modelBuilder.Entity<Log>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Log>().HasData(
                new Log { Id = 1, UserId = 1, Action = "Created a booking", Timestamp = DateTime.UtcNow },
                new Log { Id = 2, UserId = 2, Action = "Updated room details", Timestamp = DateTime.UtcNow }
            );

            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { Id = 1, Name = "Towels", Quantity = 50, LastUpdated = DateTime.UtcNow },
                new Inventory { Id = 2, Name = "Shampoo", Quantity = 30, LastUpdated = DateTime.UtcNow },
                new Inventory { Id = 3, Name = "Soap", Quantity = 100, LastUpdated = DateTime.UtcNow }
            );

            modelBuilder.Entity<MaintenanceRequest>()
                .HasOne(m => m.Room)
                .WithMany()
                .HasForeignKey(m => m.RoomId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<MaintenanceRequest>().HasData(
                new MaintenanceRequest
                {
                    Id = 1,
                    RoomId = 1,
                    Description = "Air conditioning not working",
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow
                },
                new MaintenanceRequest
                {
                    Id = 2,
                    RoomId = 2,
                    Description = "Leaking faucet in bathroom",
                    Status = "In Progress",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            // Shifts -> Employees
            modelBuilder.Entity<Shift>()
                .HasOne(s => s.Employee)
                .WithMany()
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Shift>().HasData(
                new Shift
                {
                    Id = 1,
                    EmployeeId = 1,
                    StartTime = DateTime.SpecifyKind(new DateTime(2025, 4, 20, 8, 0, 0), DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(new DateTime(2025, 4, 20, 16, 0, 0), DateTimeKind.Utc),
                    Role = "Manager"
                },
                new Shift
                {
                    Id = 2,
                    EmployeeId = 2,
                    StartTime = DateTime.SpecifyKind(new DateTime(2025, 4, 20, 16, 0, 0), DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(new DateTime(2025, 4, 20, 23, 59, 59), DateTimeKind.Utc),
                    Role = "Receptionist"
                }
            );

            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Name = "Annual Conference",
                    Date = DateTime.SpecifyKind(new DateTime(2025, 6, 15), DateTimeKind.Utc),
                    Location = "Conference Hall A",
                    Organizer = "John Doe",
                    Description = "An annual conference for industry professionals."
                },
                new Event
                {
                    Id = 2,
                    Name = "Wedding Ceremony",
                    Date = DateTime.SpecifyKind(new DateTime(2025, 7, 20), DateTimeKind.Utc),
                    Location = "Banquet Hall",
                    Organizer = "Jane Smith",
                    Description = "A beautiful wedding ceremony for 200 guests."
                }
            );

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Employee)
                .WithMany()
                .HasForeignKey(f => f.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Feedback>().HasData(
                new Feedback
                {
                    Id = 1,
                    EmployeeId = 1,
                    Message = "The new booking system is very efficient.",
                    CreatedAt = DateTime.UtcNow
                },
                new Feedback
                {
                    Id = 2,
                    EmployeeId = 2,
                    Message = "We need more training on the inventory management system.",
                    CreatedAt = DateTime.UtcNow
                }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
