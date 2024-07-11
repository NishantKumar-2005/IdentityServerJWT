using Squib.UserService.API.Model;

namespace Squib.UserService.API.Repository;

public interface IUserRepo
{
    public List<UserDto> GetUsers();
    public UserDto GetUserById(int id);

    public void AddUser(UserDto user);

    public void UpdateUser(UserDto user);

    public void DeleteUser(int id);



}
