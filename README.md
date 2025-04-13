# HotelBookingApi

HotelBookingApi is a RESTful API for managing hotel bookings and user authentication. The API allows users to register, login, and manage hotel bookings. It also includes role-based authorization to restrict access to certain endpoints.

---

## Features

- User registration and login
- Role-based authorization (Admin and User roles)
- CRUD operations for hotel bookings
- JWT authentication
- PostgreSQL database support
- Swagger for API documentation

---

## Technologies Used

- ASP.NET Core 8
- Entity Framework Core
- PostgreSQL
- JWT (JSON Web Tokens)
- Swagger for API documentation

---

## Getting Started

### Prerequisites

- .NET 8 SDK
- PostgreSQL

---

### Installation

1. **Clone the repository**:
git clone https://github.com/yourusername/HotelBookingApi.git cd HotelBookingApi

2. **Update the connection string in `appsettings.json`**:
"ConnectionStrings": { "DefaultConnection": "Host=localhost;Port=5432;Database=HotelBookingDB;Username=postgres;Password=your_password" }, "Jwt": { "Key": "your_super_secret_key12345678901" }

3. **Apply the migrations to create the database schema**:
dotnet ef database update
4. **Run the application**:
dotnet run

---

### Usage

The API provides the following endpoints:

#### Authentication

- `POST /api/Auth/Register`: Register a new user
- `POST /api/Auth/Login`: Login and get a JWT token

#### Users

- `GET /api/Auth/GetAllUsers`: Get all users (Admin only)

#### Bookings

- `GET /api/HotelBooking/GetAll`: Get all bookings
- `GET /api/HotelBooking/Get/{id}`: Get a booking by ID
- `POST /api/HotelBooking/Create`: Create a new booking (Admin only)
- `PUT /api/HotelBooking/Edit/{id}`: Edit an existing booking (Admin only)
- `DELETE /api/HotelBooking/Delete/{id}`: Delete a booking (Admin only)
- `GET /api/HotelBooking/GetAllPaged`: Get bookings with pagination

---

## Notes

- **JWT Key**: Update the `Jwt:Key` in `appsettings.json` with a secure key.
- **Admin Role**: Only users with the Admin role can access certain endpoints (e.g., creating, editing, or deleting bookings).

---

## Contributing

If you want to contribute to this project:
1. Fork the repository.
2. Create a new branch.
3. Make your changes and submit a pull request.

---

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.
