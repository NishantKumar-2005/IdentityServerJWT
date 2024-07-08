using Squib.UserService.API.Model;

namespace Squib.UserService.API.Service;

public interface IUSER_Service
{
    public List<UserDto> GetUsers();
    public UserDto GetUserById(int id);

    // Add other methods (e.g., GetUserById, CreateUser, etc.)
}
