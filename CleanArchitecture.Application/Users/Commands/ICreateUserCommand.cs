using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Users.Commands;

// The purpose of ICreateUserCommand is to handle the specific action of creating a user, 
// including any rules or validations required before actually saving the user to the database.

// in real-world applications, you don’t always want to save a user directly without checking a few things first. For example:

// Does the user have a name?
// Does the email format look correct?
// Are there other conditions we want to enforce before saving?
// That’s where ICreateUserCommand comes in, the actual business logic. It’s the one that knows how to create a user,
// including any rules or validations required before actually saving the user to the database.

public interface ICreateUserCommand
{
    Task ExecuteAsync(User user);
}
