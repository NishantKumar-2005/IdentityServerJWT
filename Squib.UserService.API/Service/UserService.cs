
using Squib.UserService.API.Model;
using Squib.UserService.API.Repository;
using Squib.UserService.API.Service.Interface;

namespace Squib.UserService.API;

public class UserService : IUSER_Service
{
    private readonly IUserRepo _userRepo;
    
    public UserService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public List<UserDto> GetUsers()
    {
       return _userRepo.GetUsers();
    }
}
