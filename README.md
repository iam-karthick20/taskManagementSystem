
# Task Management System

A modular and scalable **Task Management System** built using **Clean Architecture** with ASP.NET Core, Entity Framework Core, JWT Authentication, and API documentation via **Swagger** and **Scalar**.


# Project Structure (Clean Architecture)
    TMS.API (Controllers, Logs, Mappings, Middleware, Models)
    TMS.Application (Dtos, Exceptions)
    TMS.Domain (Entities, Interfaces)
    TMS.Infrastructure (Data - DbContext EF, DependencyInjection, Migrations, Services)

---

## Installed NuGet Packages

### TMS.API

Microsoft.AspNetCore.OpenApi
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Tools
Scalar.AspNetCore
Serilog.AspNetCore

### TMS.Infrastructure

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
System.IdentityModel.Tokens.Jwt
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.AspNetCore.Identity

---

## Project References

`TMS.API` → references `TMS.Infrastructure`
`TMS.Infrastructure` → references `TMS.Application`, `TMS.Domain`
`TMS.Domain` → references `TMS.Application`

---

## Project Setup Instructions

### Prerequisites

.NET 8 SDK
SQL Server
Visual Studio or VS Code

### Steps

1. **Clone the repository**
   
   git clone <repo-url>
   cd TMS
   

2. **Update appsettings.json** with your connection string and JWT settings if needed.

3. **Run EF Migrations**
   
   dotnet ef migrations add Initial --project TMS.Infrastructure --startup-project TMS.API --context ProjectDbContext
   dotnet ef database update --project TMS.Infrastructure --startup-project TMS.API --context ProjectDbContext
   

4. **Run the API**
   
   dotnet run --project TMS.API
   

---

## Authentication (JWT)

JWT Bearer token authentication is used.

### Sample JWT Settings in appsettings.json:

"JwtSettings": {
  "SecretKey": "***********",
  "Issuer": "KarthickRamesh",
  "Audience": "ItsForTheWorld",
  "ExpirationDays": 1,
  "RefreshTokenExpirationDays": 7
}

---

## Credentials for Test Users

Username - admin
Password - password 

### (Role can get the from claims in the JWT token)

Use these credentials to generate a token using the `/api/auth/login` endpoint.

---

## API Usage Guide

### AuthController


`/api/auth/login` - POST - Login and receive JWT         
`/api/auth/refresh-token` - POST - Refresh JWT token  
`/api/auth/logout` - POST - Logout and revoke refresh token

### UserController

`/api/user` - POST - Create new user
`/api/user` - GET - Admin Authentication Required - Get all users 
`/api/user/{userId}` - GET - User Authentication Required - Get specific user details

### TaskController

`/api/task`  - POST - User Authentication Required - Create a new task
`/api/task`  - PATCH - User / Admin Authentication Required - Update an existing task - (Admin can complete and User can Update)
`/api/task`  - GET - User / Admin Authentication Required - Retrieve user tasks (Admin can get all task and User can get their own task)

---

## 🧾 Task Entity

Property       Type         Description               
----------------------------------------------------------------              
Id             int          Unique identifier                       
Title          string       Required short description              
Description    string       Optional, longer description            
IsCompleted    bool         Indicates if task is complete           
CreatedAt      DateTime     Automatically set when task is created  
DueDate        DateTime?    Optional deadline                       
OwnerUserId    string       Represents the user who owns the task   

### User Entity
Property	    Type	    Description
---------------------------------------------------------------------
UserId	        Guid	    Unique identifier for the user
FirstName	    string?	    User's first name (optional)
LastName	    string?	    User's last name (optional)
Username	    string?	    Username (max length: 100 characters)
PasswordHash	string?	    Hashed password (max length: 2000 chars)
Role	        string?	    Role of the user (e.g., Admin, User)
EmailAddress	string?	    Email address (max length: 100 characters)
IsActive	    bool	    Indicates if the user is active
CreatedOn	    DateTime	Date the user was created

### UserLoginHistory Entity
Property	Type	Description
----------------------------------------------------------------------
Id	                    int	        Auto-incrementing unique ID
UserId	                Guid	    Foreign key reference to the User entity
LoginDatetime	        DateTime	Date and time the user logged in
LogoutDatetime	        DateTime?	Date and time the user logged out (optional)
RefreshToken	        string	    Refresh token used during login (max 500 chars)
RefreshTokenExpiryTime	DateTime	Expiration time of the refresh token
IsRevoked	            bool	    Indicates if the token was revoked
RevokedAt	            DateTime?	Date and time the token was revoked (optional)
---

## Logging (Serilog)

General logs: `logs/general-log-.txt`
Error logs: `logs/error-log-.txt`
Rolling interval: Daily

---

## API Documentation

### Scalar API Reference
https://localhost:<port>/scalar


---

## Getting Started (Quick Run)

dotnet restore
dotnet build
dotnet run --project TMS.API

---




