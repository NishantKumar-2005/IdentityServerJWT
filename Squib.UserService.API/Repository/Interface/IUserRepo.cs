using Squib.UserService.API.model;
using Squib.UserService.API.Model;

namespace Squib.UserService.API.Repository;

public interface IUserRepo
{
    public List<UserDto> GetUsers();
    public UserDto GetUserById(int id);

    public bool AddUser(UserDto user);

    public bool UpdateUser(UserDto user);

    public bool DeleteUser(int id);

   



}
