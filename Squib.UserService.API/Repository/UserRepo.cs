using Squib.UserService.API.Model;
using Squib.UserService.API.Repository;
namespace Squib.UserService.API;

public class UserRepo : IUserRepo
{
    
    List<UserDto> UserData;

        UserRepo(IUserRepo userRepo)
        {
            UserData = new List<UserDto>
            {
                new UserDto { Id = 1, Email = "helrar", FirstName = "Helrar" },
                new UserDto { Id = 2, Email = "johndoe", FirstName = "John" },
                
                };
        }
    public List<UserDto> GetUsers(){
        return UserData;
    }
    
}
