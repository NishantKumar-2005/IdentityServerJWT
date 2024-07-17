using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Squib.UserService.API.config;
using Squib.UserService.API.model;

namespace Squib.UserService.API;

public class OrderRepo : IOrderRepo
{
    private readonly ILogger<UserRepo> _logger;
    private readonly ConnectionString _connectionString;

    public OrderRepo(ILogger<UserRepo> logger, IOptions<ConnectionString> connectionStringOption)
    {
        _logger = logger;
        _connectionString = connectionStringOption.Value;
    }
    List<Order> orders = new List<Order>();

    public List<Order> GetOrders()
    {
        try{
        using var connection = new SqlConnection(_connectionString.MyDb);
        using var commad = connection.CreateCommand();
        commad.CommandType = CommandType.Text;
        commad.CommandText = "SELECT * FROM Orders";
        commad.Connection = connection;
        connection.Open();
        using var reader = commad.ExecuteReader();
        
        while (reader.Read())
        {
            orders.Add(new Order
            {
                OrderId = reader.GetInt32(0),
                ProductName = reader.GetString(1)
            });
        }
        }
        catch(Exception e){
            _logger.LogError($"{e.Message}\n{e.StackTrace}");
        }
        
        return orders;

    }
    public Order GetOrderById(int orderId)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString.MyDb);
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"SELECT * FROM Orders WHERE OrderId = {orderId}";
            command.Connection = connection;
            connection.Open();
            using var reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                return new Order
                {
                    OrderId = reader.GetInt32(0),
                    ProductName = reader.GetString(1)
                };
            }
        }
        catch (Exception e)
        {
            
         _logger.LogError($"{e.Message}\n{e.StackTrace}");
        }
        return null;
    }

    public void AddOrder(Order order)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString.MyDb);
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"INSERT INTO Orders (ProductName) VALUES ('{order.ProductName}')";
            command.Connection = connection;
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            
         _logger.LogError($"{e.Message}\n{e.StackTrace}");
        }
    }
    public void UpdateOrder(Order order)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString.MyDb);
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
             command.CommandText = "UPDATE Orders SET ProductName = @ProductName WHERE OrderId = @OrderId";
            command.Parameters.AddWithValue("@ProductName", order.ProductName);
            command.Parameters.AddWithValue("@OrderId", order.OrderId);
            command.Connection = connection;
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            
         _logger.LogError($"{e.Message}\n{e.StackTrace}");
        }
    }
    public void DeleteOrder(int orderId)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString.MyDb);
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"DELETE FROM Orders WHERE OrderId = {orderId}";
            command.Connection = connection;
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            
         _logger.LogError($"{e.Message}\n{e.StackTrace}");
        }
    }
}
