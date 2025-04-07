# HotelBookingApi

HotelBookingApi is a RESTful API for managing hotel bookings and user authentication. The API allows users to register, login, and manage hotel bookings. It also includes role-based authorization to restrict access to certain endpoints.

## Features

- User registration and login
- Role-based authorization (Admin and User roles)
- CRUD operations for hotel bookings
- Caching for improved performance
- JWT authentication

## Technologies Used

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- JWT (JSON Web Tokens)
- In-Memory Caching
- Swagger for API documentation

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server

### Installation

1. Clone the repository:
git clone https://github.com/yourusername/HotelBookingApi.git cd HotelBookingApi
2. Update the connection string in `appsettings.json`:
"ConnectionStrings": { "DefaultConnection": "Server=your_server_name;Database=HotelBookingDb;Trusted_Connection=True;MultipleActiveResultSets=true" }, "Jwt": { "Key": "your_super_secret_key12345678901" }
3. Apply the migrations to create the database schema:
dotnet ef database update
4. Run the application:
dotnet run


### Usage

The API provides the following endpoints:

#### Authentication

- `POST /api/Auth/Register`: Register a new user
- `POST /api/Auth/Login`: Login and get a JWT token

#### Users

- `GET /api/Auth/GetAllUsers`: Get all users (Admin only)

#### Bookings

- `GET /api/HotelBooking/GetAll`: Get all bookings
- `POST /api/HotelBooking/CreateEdit`: Create or edit a booking (Admin only)
- `GET /api/HotelBooking/Get/{id}`: Get a booking by ID
- `DELETE /api/HotelBooking/Delete/{id}`: Delete a booking (Admin only)
