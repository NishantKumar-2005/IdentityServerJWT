using Squib.UserService.API.Model;

namespace Squib.UserService.API.Service.Interface;

public interface IUSER_Service
{
    public List<UserDto> GetUsers();
}
