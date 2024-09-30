using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squib.UserService.API.Model;
using Squib.UserService.API.Service;


[ApiController]
[Route("api/[controller]")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class UserController : ControllerBase
{
    private readonly IUSER_Service _userService;

    public UserController(IUSER_Service userService)
    {
        _userService = userService;
    }

    [HttpGet]
[HttpGet]
public async Task<IActionResult> GetAllUsers()
{
    var users = await _userService.GetUsers();
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


    [HttpPost]
public  async Task<IActionResult> AddUser([FromBody] UserDto user)
{
    await _userService.AddUser(user);
    return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
}
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserDto user)
    {
        if (id!= user.Id)
        {
            return BadRequest();
        }
        _userService.UpdateUser(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        _userService.DeleteUser(id);
        return NoContent();
    }



   

    
}
