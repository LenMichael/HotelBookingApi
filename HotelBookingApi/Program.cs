using Microsoft.EntityFrameworkCore;
using HotelBookingApi.Data;
using HotelBookingApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;
using HotelBookingApi.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
builder.Services.AddScoped<IHotelBookingRepository, HotelBookingRepository>();
builder.Services.AddScoped<IHotelBookingService, HotelBookingService>();
var useInMemoryDb = false;

try
{
    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApiContext>();
        context.Database.EnsureCreated();
    }
}
catch
{
    useInMemoryDb = true;
}

if (useInMemoryDb)
{
    builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("BookingDb"));
    Console.WriteLine("Using InMemory database");
}
else
{
    Console.WriteLine("Using SQL Server database");
}

//builder.Services.AddDbContext<ApiContext>(opt =>
//    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("BookingDb"));

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "HotelBookingApi",
        Version = "v1"
    });

    // Προσθήκη υποστήριξης για Authorization
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

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
        ValidateAudience = false,
        RoleClaimType = "role"
    };
});

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminPolicy", policy =>
//        policy.RequireRole("Admin"));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var allowedOrigins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>();

if (!allowedOrigins.IsNullOrEmpty())
{
    app.UseCors(builder =>
        builder.WithOrigins(allowedOrigins)
               .AllowAnyMethod()
               .AllowAnyHeader());
}



app.UseHttpsRedirection();
app.UseAuthentication();
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


    if (useInMemoryDb)
    {
        // Initialize Bookings data in in-memory database
        if (!context.Bookings.Any())
        {
            context.Bookings.AddRange(new List<HotelBooking>
            {
                new HotelBooking { RoomNumber = 101, ClientName = "John Doe" },
                new HotelBooking { RoomNumber = 102, ClientName = "Jane Smith" },
                new HotelBooking { RoomNumber = 103, ClientName = "Alice Brown" }
            });
            context.SaveChanges();
        }
    }

    // Initialize cache
    var bookings = context.Bookings.ToList();
    cache.Set("bookings", bookings);

    // Initialize Users data
    if (!context.Users.Any())
    {
        context.Users.AddRange(new List<User>
        {
            new User { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = "Admin" },
            new User { Username = "user", PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"), Role = "User" }
        });
        context.SaveChanges();
    }

    var users = context.Users.ToList();
    foreach (var user in users)
    {
        cache.Set(user.Username, user);
    }
    #endregion

}

app.Run();
