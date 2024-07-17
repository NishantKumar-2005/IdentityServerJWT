namespace Squib.UserService.API.model;

public record class Order
{
    public int OrderId { get; set; }
    public string ProductName { get; set; }
}
