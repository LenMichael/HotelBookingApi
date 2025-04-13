﻿using Microsoft.EntityFrameworkCore;
using HotelBookingApi.Data;
using HotelBookingApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;
using HotelBookingApi.Services;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Configure Database Context
// ----------------------------

//builder.Services.AddDbContext<ApiContext>(opt =>
//    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApiContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.Run();
