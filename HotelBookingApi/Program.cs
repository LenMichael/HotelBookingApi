using Microsoft.EntityFrameworkCore;
using HotelBookingApi.Data;
using HotelBookingApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using HotelBookingApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApiContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("BookingDb"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
    builder.WithOrigins("http://localhost:5173") 
           .AllowAnyMethod()
           .AllowAnyHeader());



app.UseHttpsRedirection();
app.UseAuthorization();

//app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

#region Sample data, no need DB
// Initialize cache
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApiContext>();
    var cache = services.GetRequiredService<IMemoryCache>();
    // Initialize Bookings data
    var bookings = new List<HotelBooking>
    {
        new HotelBooking { Id = 1, RoomNumber = 101, ClientName = "John Doe" },
        new HotelBooking { Id = 2, RoomNumber = 102, ClientName = "Jane Smith" }
    };
    //context.Bookings.AddRange(bookings);
    //context.SaveChanges();
    cache.Set("bookings", bookings);

    // Initialize Users data
    var users = new List<User>
    {
        new User { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = "Admin" },
        new User { Username = "user", PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"), Role = "User" }
    };
    //context.Users.AddRange(users);
    //context.SaveChanges();
    foreach (var user in users)
    {
        cache.Set(user.Username, user);
    }
#endregion

}

app.Run();
