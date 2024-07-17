using Squib.UserService.API.model;

namespace Squib.UserService.API;

public class OrderServi : IORDER_Service
{
    private readonly IOrderRepo _orderRepo;
    
    public OrderServi(IOrderRepo orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public List<Order> GetOrders(){
        return _orderRepo.GetOrders();
    }
    public Order GetOrderById(int id){
        return _orderRepo.GetOrderById(id);
    }
    public void AddOrder(Order order){
        _orderRepo.AddOrder(order);
    }
    public void UpdateOrder(Order order){
        _orderRepo.UpdateOrder(order);
    }
    public void DeleteOrder(int id){
        _orderRepo.DeleteOrder(id);
    }
}
