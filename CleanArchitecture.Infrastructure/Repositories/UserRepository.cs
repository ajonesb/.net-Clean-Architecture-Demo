using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Repositories;

// The term repository comes from a design pattern called the Repository Pattern. 
// The idea is to create a middle layer between your application's business logic 
// and the database. Instead of directly interacting with the database 
// (SQL queries or ORM), your application uses a repository to access or modify data.

// The repository is a collection-like abstraction that handles data-related operations 
// (e.g., fetching, saving, deleting).
// You can think of it as a gateway to the database that simplifies data access and 
// keeps database code in one place.
// The repository itself is not business logic. Instead, it supports the business logic
// by handling data persistence and retrieval.
public class UserRepository : IUserRepository
{
    public async Task AddUserAsync(User user)
    {
        Console.WriteLine($"User {user.Name} added.");
        await Task.CompletedTask;
    }
}
