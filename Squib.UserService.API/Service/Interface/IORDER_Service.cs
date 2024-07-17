using Squib.UserService.API.model;

namespace Squib.UserService.API;

public interface IORDER_Service
{
    public List<Order> GetOrders();
    public Order GetOrderById(int id);
    public void AddOrder(Order order);
    public void UpdateOrder(Order order);
    public void DeleteOrder(int id);
}
