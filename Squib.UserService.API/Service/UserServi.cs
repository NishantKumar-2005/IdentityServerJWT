using AutoMapper;
using Newtonsoft.Json;
using Squib.UserService.API.model;
using Squib.UserService.API.Model;
using Squib.UserService.API.Repository;
namespace Squib.UserService.API.Service;

public class UserServi: IUSER_Service
{
    private readonly IUserRepo _userRepo;
    private readonly IMapper _mapper;
    
    public UserServi(IUserRepo userRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
        
    }

    public List<UserRDto> GetUsers()
    {
        var UserData =_userRepo.GetUsers();
        // var UserRdata = _mapper.Map<UserRDto>(UserData);
        var UserRdata = _mapper.Map<List<UserRDto>>(UserData);
        
        string jsonString = JsonConvert.SerializeObject(UserRdata);
        Console.WriteLine("Serialized JSON string (Newtonsoft.Json):");
        Console.WriteLine(jsonString);

        var deserializedUserDto = JsonConvert.DeserializeObject<List<UserDto>>(jsonString);
        Console.WriteLine("\nDeserialized UserDto object (Newtonsoft.Json):");
        Console.WriteLine($"Id: {deserializedUserDto[0].Id}");
        Console.WriteLine($"Email: {deserializedUserDto[0].Email}");
        Console.WriteLine($"FirstName: {deserializedUserDto[0].FirstName}");
        Console.WriteLine($"LastName: {deserializedUserDto[0].LastName}");

        
       return UserRdata;
    }

    public UserDto GetUserById(int id)
    {
        return _userRepo.GetUserById(id);
    }

    public bool UpdateUser(UserDto user){
        return _userRepo.UpdateUser(user);
    }
    public bool DeleteUser(int id){
        return _userRepo.DeleteUser(id);
    }
    public bool AddUser(UserDto user){
        return _userRepo.AddUser(user);
    }

    


}
