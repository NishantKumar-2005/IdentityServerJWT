using System.Data;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Squib.UserService.API.config;
using Squib.UserService.API.model;
using Squib.UserService.API.Model;
using Squib.UserService.API.Repository;
namespace Squib.UserService.API;

public class UserRepo : IUserRepo
{
    private readonly ILogger<UserRepo> _logger;
    private readonly ConnectionString _connectionString;

    private readonly IMapper _mapper;

    public UserRepo(ILogger<UserRepo> logger, IOptions<ConnectionString> connectionStringOption , IMapper mapper)
    {
        _logger = logger;
        _connectionString = connectionStringOption.Value;
        _mapper = mapper;
    }
    public List<UserRDto> GetUsers()
    {
        List<UserDto> UserData = new List<UserDto>();
        // Add logic to get users from the database
        try
        {
            using var connection = new SqlConnection(_connectionString.MyDb);
            using var commad = connection.CreateCommand();
            commad.CommandType = CommandType.Text;
            commad.CommandText = "SELECT * FROM UserDto_New";
            commad.Connection = connection;
            connection.Open();

            using var reader = commad.ExecuteReader();
            while (reader.Read())
            {
                UserData.Add(new UserDto
                {
                    Id = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    FirstName = reader.GetString(2),
                    LastName = reader.GetString(3)
                });
            }
        }
        catch (Exception e)
        {
            _logger.LogError($"{e.Message}\n{e.StackTrace}");
        }


        // var UserRdata = _mapper.Map<UserRDto>(UserData);
        var UserRdata = _mapper.Map<List<UserRDto>>(UserData);
        return UserRdata;


    }

    public UserDto GetUserById(int id)
    {
        UserDto UserData = null;
        // Add logic to get user by id from the database
        try
        {
            using var connection = new SqlConnection(_connectionString.MyDb);
            using var commad = connection.CreateCommand();
            commad.CommandType = CommandType.Text;
            commad.CommandText = "SELECT * FROM UserDto_New WHERE Id = @Id";
            commad.Parameters.AddWithValue("@Id", id);
            commad.Connection = connection;
            connection.Open();

            using var reader = commad.ExecuteReader();
            if (reader.Read())
            {
                UserData = new UserDto
                {
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
public void AddUser(UserDto user)
{
    try
    {
        using var connection = new SqlConnection(_connectionString.MyDb);
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;

        // Updated SQL command to exclude the ID column
        command.CommandText = "INSERT INTO UserDto_New (Email, FirstName , LastName) VALUES (@Email, @FirstName,@LastName)";
        command.Parameters.AddWithValue("@Email", user.Email);
        command.Parameters.AddWithValue("@FirstName", user.FirstName);
        command.Parameters.AddWithValue("@LastName", user.LastName);
        command.Connection = connection;
        connection.Open();
        command.ExecuteNonQuery();
    }
    catch (Exception e)
    {
        _logger.LogError($"{e.Message}\n{e.StackTrace}");
    }
}


    public void UpdateUser(UserDto user)
    {
        // Add logic to update user in the database
        try
        {
            using var connection = new SqlConnection(_connectionString.MyDb);
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE UserDto_New SET Email = @Email, FirstName = @FirstName,LastName=@LastName WHERE Id = @Id";
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@LastName", user.LastName);
            command.Connection = connection;
            connection.Open();
            command.ExecuteNonQuery();
    }
    catch (Exception e)
        {
            _logger.LogError($"{e.Message}\n{e.StackTrace}");
        }
    }

    public void DeleteUser(int id)
    {
        // Add logic to delete user from the database
        try
        {
            using var connection = new SqlConnection(_connectionString.MyDb);
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM UserDto_New WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);
            command.Connection = connection;
            connection.Open();
            command.ExecuteNonQuery();
    }catch (Exception e){
        _logger.LogError($"{e.Message}\n{e.StackTrace}");
    
    }
    }

    
}
