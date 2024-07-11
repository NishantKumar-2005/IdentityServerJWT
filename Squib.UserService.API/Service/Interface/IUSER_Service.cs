using Squib.UserService.API.Model;

namespace Squib.UserService.API.Service;

public interface IUSER_Service
{
    public List<UserDto> GetUsers();
    public UserDto GetUserById(int id);

    public void AddUser(UserDto user);

    public void UpdateUser(UserDto user);

    public void DeleteUser(int id);

    

    // Add other methods (e.g., GetUserById, CreateUser, etc.)
}
