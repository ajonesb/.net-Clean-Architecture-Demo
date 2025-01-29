using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Interfaces;

// IUserRepository is like a toolbox. It knows how to save a user in the database 
// (using AddUserAsync), but it doesn’t care about rules or whether the user data is valid or not. It just does what it's told—"add this user."

public interface IUserRepository
{
    Task AddUserAsync(User user);
}
