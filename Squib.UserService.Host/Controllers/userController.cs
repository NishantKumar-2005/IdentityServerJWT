using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Squib.UserService.API.Service;


[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IUSER_Service _userService;

    public UserController(IUSER_Service userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

   

    // Add other actions (e.g., GetUserById, CreateUser, etc.)
}
