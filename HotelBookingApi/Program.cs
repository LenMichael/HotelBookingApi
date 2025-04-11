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
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;


var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Configure Database Context
// ----------------------------
builder.Services.AddDbContext<ApiContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var useInMemoryDb = false;
try
{
    // Verify that the SQL Server database can be created/connected
    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApiContext>();
        context.Database.EnsureCreated();
    }
}
catch
{
    // If an exception occurs (e.g. SQL Server unreachable), fall back to InMemory database
    useInMemoryDb = true;
    builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("BookingDb"));
    Console.WriteLine("Using InMemory database");
}

if (!useInMemoryDb)
    Console.WriteLine("Using SQL Server database");


// ------------------------------
// Register Application Services
// ------------------------------
builder.Services.AddScoped<IHotelBookingRepository, HotelBookingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHotelBookingService, HotelBookingService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

// -------------------------------
// Configure Swagger/OpenAPI
// -------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HotelBookingApi",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// ------------------------------
// Configure Authentication
// ------------------------------
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

// ------------------------------
// Build the application
// ------------------------------
var app = builder.Build();

// -----------------------------
// Configure Middleware Pipeline
// -----------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS if allowed origins are configured in appsettings.json
var allowedOrigins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>();

if (!allowedOrigins.IsNullOrEmpty() && allowedOrigins.Any())
{
    app.UseCors(builder =>
        builder.WithOrigins(allowedOrigins)
               .AllowAnyMethod()
               .AllowAnyHeader());
}



app.UseHttpsRedirection();
app.UseAuthentication();

app.Use(async (context, next) =>
{
    var user = context.User;
    //For debbuging: place a breakpoint on the line below
    //System.Diagnostics.Debugger.Break();

    //Optionally log the claims
    var claims = user.Claims.Select(c => new { c.Type, c.Value }).ToList();
    if (!claims.IsNullOrEmpty())
    {
        foreach (var claim in claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
        }
    }
    await next.Invoke();
});

app.UseAuthorization();

// Uncomment the following line if you have a custom exception handling middleware
//app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

#region Sample data, no need DB
// -------------------------------------------
// Seed Sample Data and Cache Initialization
// -------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApiContext>();
    var cache = services.GetRequiredService<IMemoryCache>();


    // Seed Bookings data in in-memory database
    if (useInMemoryDb)
    {
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

    // Cache Bookings data
    var bookings = context.Bookings.ToList();
    cache.Set("bookings", bookings);

    // Seed Users if no users exist
    if (!context.Users.Any())
    {
        context.Users.AddRange(new List<User>
        {
            new User { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = "Admin" },
            new User { Username = "user", PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"), Role = "User" }
        });
        context.SaveChanges();
    }

    // Cache each user by username for quick lookup
    var users = context.Users.ToList();
    foreach (var user in users)
    {
        cache.Set(user.Username, user);
    }
    #endregion
}

app.Run();
