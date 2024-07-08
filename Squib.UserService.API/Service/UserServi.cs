
using Squib.UserService.API.Model;
using Squib.UserService.API.Repository;
namespace Squib.UserService.API.Service;

public class UserServi: IUSER_Service
{
    private readonly IUserRepo _userRepo;
    
    public UserServi(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public List<UserDto> GetUsers()
    {
       return _userRepo.GetUsers();
    }

    public UserDto GetUserById(int id)
    {
        return _userRepo.GetUserById(id);
    }
}
