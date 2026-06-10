using DeliverySystem.Models;
using System.Data;

class Order
{
    public int OrderId { get; set; }
    public DateTime CreateDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public int CourierId { get; set; }
    public int CustomerId { get; set; }
}