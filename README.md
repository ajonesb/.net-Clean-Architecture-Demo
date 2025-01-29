# Clean Architecture API Example

This project demonstrates a practical implementation of **Clean Architecture** in a .NET-based Web API. It showcases how to structure a maintainable, scalable, and testable application by separating concerns into multiple layers.

## Overview

The Clean Architecture design pattern promotes separation of concerns by organizing the application into distinct layers: **Domain**, **Application**, **Infrastructure**, and **Presentation**. This approach ensures that the core business logic remains isolated from external dependencies.

### Why Clean Architecture?

- **Separation of Concerns**: Each layer has a specific responsibility.
- **Scalability**: Easily add new features without affecting the core logic.
- **Testability**: Business rules are isolated, making them easier to test.
- **Maintainability**: Clear structure and boundaries simplify long-term maintenance.

---

## Features

- **Health Check Endpoint**: `GET /api/users` confirms the API is running.
- **User Creation Endpoint**: `POST /api/users` allows adding new users.
- **Console Logging**: Simulates data persistence by logging added users to the console.

---

## Folder Structure

```
CleanArchitectureDemo/
│
├── CleanArchitecture.Domain/
│   └── Entities/
│       └── User.cs
│
├── CleanArchitecture.Application/
│   ├── Interfaces/
│   │   └── IUserRepository.cs
│   └── Users/
│       └── Commands/
│           ├── CreateUserCommand.cs
│           └── ICreateUserCommand.cs
│
├── CleanArchitecture.Infrastructure/
│   └── Repositories/
│       └── UserRepository.cs
│
├── CleanArchitecture.Presentation/
│   ├── Controllers/
│   │   └── UsersController.cs
│   └── Program.cs
└── CleanArchitectureExample.sln
```

---

## Components and Code Details

### 1. Domain Layer

Defines the core business entities and rules.

**`User.cs`**

```csharp
namespace CleanArchitecture.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
```

---

### 2. Application Layer

Contains business use cases and orchestrates application logic.

**`IUserRepository.cs`**

```csharp
namespace CleanArchitecture.Application.Interfaces;

public interface IUserRepository
{
    Task AddUserAsync(User user);
}
```

**`CreateUserCommand.cs`**

```csharp
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Users.Commands;

public class CreateUserCommand : ICreateUserCommand
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommand(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync(User user)
    {
        if (string.IsNullOrEmpty(user.Name))
            throw new ArgumentException("User name is required.");
        await _userRepository.AddUserAsync(user);
    }
}
```

---

### 3. Infrastructure Layer

Implements the `IUserRepository` interface for data persistence.

**`UserRepository.cs`**

```csharp
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public async Task AddUserAsync(User user)
    {
        Console.WriteLine($"User {user.Name} added.");
        await Task.CompletedTask;
    }
}
```

---

### 4. Presentation Layer

Exposes the application via API endpoints.

**`UsersController.cs`**

```csharp
using CleanArchitecture.Application.Users.Commands;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ICreateUserCommand _createUserCommand;

    public UsersController(ICreateUserCommand createUserCommand)
    {
        _createUserCommand = createUserCommand;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(new { message = "Users API is working!" });
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        try
        {
            await _createUserCommand.ExecuteAsync(user);
            return Ok("User created successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
```

**`Program.cs`**

```csharp
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Users.Commands;
using CleanArchitecture.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICreateUserCommand, CreateUserCommand>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

---

## How to Run the Project

### Prerequisites

- Install the [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download).
- Use a development environment like Visual Studio Code or JetBrains Rider.

### Setup Instructions

1. Clone this repository:

   `git clone <repository-url>`
   `cd CleanArchitectureDemo`

2. Restore dependencies:

   `dotnet restore`

3. Build the project:

   `dotnet build`

4. Run the application:

   `dotnet run --project CleanArchitecture.Presentation`

5. Access the API:
   - Open [http://localhost:5258/api/users](http://localhost:5258/api/users) to check the health endpoint.
   - Use Postman or a similar tool to test the `POST /api/users` endpoint.
   - Swagger : `http://localhost:5258/swagger`

---

## Troubleshooting and Lessons Learned

### Common Issues and Fixes

1. **Namespace Errors**:

   - Missing `using` directives caused build errors. Fixed by ensuring all namespaces were correctly imported.

2. **Unregistered Dependencies**:

   - `IUserRepository` and `ICreateUserCommand` were not registered in `Program.cs`. Resolved by adding them to the DI container.

3. **HTTPS Warning**:

   - Added `UseHttpsRedirection` in `Program.cs` to resolve startup warnings.

4. **Missing Endpoints**:
   - Initially, the `GET` endpoint for users was missing. Added a health check endpoint in `UsersController`.

---

## Example Requests

### GET `/api/users`

Response:

```json
{
  "message": "Users API is working!"
}
```

### POST `/api/users`

Request:

```json
{
  "name": "John Doe",
  "email": "john.doe@example.com"
}
```

Response:

```json
"User created successfully."
```

---

## License

This project is licensed under the MIT License. Feel free to use and modify it.

---

## Acknowledgments

Special thanks to the developers and architects contributing to best practices in software design and development.
