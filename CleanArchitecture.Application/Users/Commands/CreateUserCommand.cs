/// <summary>
/// Command to create a new user.
/// </summary>
/// <remarks>
/// This command is part of the application layer in the Clean Architecture.
/// It handles the creation of a new user by interacting with the user repository.
/// </remarks>
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
