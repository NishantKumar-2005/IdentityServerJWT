using System.Data;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Squib.UserService.API.config;
using Squib.UserService.API.model;
using Squib.UserService.API.Model;
using Squib.UserService.API.Repository;
namespace Squib.UserService.API;

public class UserRepo : IUserRepo
{
    private readonly ILogger<UserRepo> _logger;
    private readonly ConnectionString _connectionString;

    

    public UserRepo(ILogger<UserRepo> logger, IOptions<ConnectionString> connectionStringOption )
    {
        _logger = logger;
        _connectionString = connectionStringOption.Value;
        
    }
    public async Task<List<UserDto>> GetUsers()
{
    List<UserDto> userData = new List<UserDto>();
    // Add logic to get users from the database
    try
    {
        using var connection = new SqlConnection(_connectionString.MyDb);
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "SELECT * FROM UserDto_New";
        command.Connection = connection;
        
        // Open connection asynchronously
        await connection.OpenAsync();

        // Execute reader asynchronously
        using var reader = await command.ExecuteReaderAsync();
        
        while (await reader.ReadAsync()) // Asynchronous read
        {
            userData.Add(new UserDto
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

    return userData;
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
                    LastName = reader.GetString(3)
                };
            }

        }
        catch (Exception e)
        {

            _logger.LogError($"{e.Message}\n{e.StackTrace}");
        }


        return UserData;
    }
public bool AddUser(UserDto user)
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
        return true;
    }
    catch (Exception e)
    {
        _logger.LogError($"{e.Message}\n{e.StackTrace}");
        return false;
    }
}


    public bool UpdateUser(UserDto user)
{
    try
    {
        using var connection = new SqlConnection(_connectionString.MyDb);
        using var command = connection.CreateCommand();
        
        command.CommandType = CommandType.Text;
        command.CommandText = "UPDATE UserDto_New SET Email = @Email, FirstName = @FirstName, LastName = @LastName WHERE Id = @Id";
        
        command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email ?? (object)DBNull.Value;
        command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = user.FirstName ?? (object)DBNull.Value;
        command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = user.LastName ?? (object)DBNull.Value;
        command.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id;

        connection.Open();
        int rowsAffected = command.ExecuteNonQuery();

        return rowsAffected > 0;
    }
    catch (Exception e)
    {
        _logger.LogError($"{e.Message}\n{e.StackTrace}");
        return false;
    }
}


    public bool DeleteUser(int id)
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
            return true;
    }catch (Exception e){
        _logger.LogError($"{e.Message}\n{e.StackTrace}");
        return false;
    
    }
    }

    
}
