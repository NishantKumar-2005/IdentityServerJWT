using Squib.UserService.API.model;
using Squib.UserService.API.Model;

namespace Squib.UserService.API.Service;

public interface IUSER_Service
{
    public Task<List<UserRDto>> GetUsers();
    public UserDto GetUserById(int id);

    public Task<bool> AddUser(UserDto user);

    public bool UpdateUser(UserDto user);

    public bool DeleteUser(int id);

    

    

    // Add other methods (e.g., GetUserById, CreateUser, etc.)
}
