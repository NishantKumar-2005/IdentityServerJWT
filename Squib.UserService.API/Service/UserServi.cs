
using Squib.UserService.API.model;
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

    public List<UserRDto> GetUsers()
    {
       return _userRepo.GetUsers();
    }

    public UserDto GetUserById(int id)
    {
        return _userRepo.GetUserById(id);
    }

    public void UpdateUser(UserDto user){
        _userRepo.UpdateUser(user);
    }
    public void DeleteUser(int id){
        _userRepo.DeleteUser(id);
    }
    public void AddUser(UserDto user){
         _userRepo.AddUser(user);
    }

    


}
