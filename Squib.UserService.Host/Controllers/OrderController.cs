using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Squib.UserService.API;
using Squib.UserService.API.model;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IORDER_Service _orderService;

    public OrderController(IORDER_Service orderService)
    {
        _orderService = orderService;
    }

[HttpGet]
public ActionResult<List<Order>> GetOrders()
{
    return Ok(_orderService.GetOrders());
}

[HttpGet("{id}")]
public ActionResult<Order> GetOrderById(int id)
{
    var order = _orderService.GetOrderById(id);
    if (order == null)
    {
        return NotFound();
    }
    return order;
}

[HttpPost]
public ActionResult<Order> AddOrder(Order order)
{
    _orderService.AddOrder(order);
    return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
}

[HttpPut("{id}")]
public ActionResult UpdateOrder(int id, Order order)
{
    if (id!= order.OrderId)
    {
        return BadRequest();
    }
    _orderService.UpdateOrder(order);
    return NoContent();
}

[HttpDelete("{id}")]
public ActionResult DeleteOrder(int id)
{
    _orderService.DeleteOrder(id);
    return NoContent();
}
}
