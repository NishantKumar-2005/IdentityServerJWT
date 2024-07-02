using Microsoft.AspNetCore.Mvc;
using Squib.UserService.Host.Services;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    public IActionResult GetUserById(int id){
        var user = _userService.GetUserById(id);
        return Ok(user);
    }

    public IActionResult CreateUser(User user){
        _userService.CreateUser(user);
        return Ok();
    }

    // Add other actions (e.g., GetUserById, CreateUser, etc.)
}
