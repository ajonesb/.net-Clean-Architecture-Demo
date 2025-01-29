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
        // This is a placeholder. Replace with actual logic to fetch users if needed.
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
