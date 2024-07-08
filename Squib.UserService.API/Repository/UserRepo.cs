using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Squib.UserService.API.config;
using Squib.UserService.API.Model;
using Squib.UserService.API.Repository;
namespace Squib.UserService.API;

public class UserRepo : IUserRepo
{
    private readonly ILogger<UserRepo> _logger;
    private readonly ConnectionString _connectionString;
    

        UserRepo(ILogger<UserRepo> logger, IOptions<ConnectionString> connectionStringOption)
        {
            _logger = logger;
            _connectionString = connectionStringOption.Value;
        }
    public List<UserDto> GetUsers(){
        List<UserDto> UserData = [];
        // Add logic to get users from the database
        try
        {
            using var connection = new SqlConnection(_connectionString.MyDb);
        using var commad = connection.CreateCommand();
        commad.CommandType = CommandType.Text;
        commad.CommandText = "SELECT * FROM Users";
        commad.Connection = connection;
        connection.Open();

        using var reader = commad.ExecuteReader();
        while(reader.Read()){
            UserData.Add(new UserDto{
                Id = reader.GetInt32(0),
                Email = reader.GetString(1),
                FirstName = reader.GetString(2),
            });
        }
        }
        catch (Exception e)
        {
            
                _logger.LogError($"{e.Message}\n{e.StackTrace}");
        }
        


        return UserData;
    }

    public UserDto GetUserById(int id){
        UserDto UserData = null;
        // Add logic to get user by id from the database
        try
        {
            using var connection = new SqlConnection(_connectionString.MyDb);
        using var commad = connection.CreateCommand();
        commad.CommandType = CommandType.Text;
        commad.CommandText = "SELECT * FROM Users WHERE Id = @Id";
        commad.Parameters.AddWithValue("@Id", id);
        commad.Connection = connection;
        connection.Open();

        using var reader = commad.ExecuteReader();
        if(reader.Read()){
            UserData = new UserDto{
                Id = reader.GetInt32(0),
                Email = reader.GetString(1),
                FirstName = reader.GetString(2),
            };
        }
            
        }
        catch (Exception e)
        {
            
             _logger.LogError($"{e.Message}\n{e.StackTrace}");
        }
        

        return UserData;
    }
    
}
