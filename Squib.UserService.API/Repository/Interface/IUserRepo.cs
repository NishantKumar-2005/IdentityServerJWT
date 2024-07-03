using Squib.UserService.API.Model;

namespace Squib.UserService.API.Repository;

public interface IUserRepo
{
    public List<UserDto> GetUsers();
}
