using DeliverySystem.Models;
using System.Data;
using System.Net.Http.Headers;

class Order
{
    public int OrderId { get; set; }
    public DateTime CreateDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public int CourierId { get; set; }
    public int CustomerId { get; set; }
    public override string ToString()
    {
        string result = 
            $"=====ЗАКАЗ=====\n" +
            $"ID заказа: {OrderId}\n" +
            $"Дата создания: {CreateDate}\n" +
            $"Статус заказа: {OrderStatus}\n" +
            $"Продукты:\n"
        ;

        foreach (var product in OrderItems)
        {
            result += product + "\n";
        }

        result += $"{CourierId}\n" +
                  $"{CustomerId}"
        ;

        return result;
    }
}