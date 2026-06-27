using DeliverySystem.Models;
using System.Data;
using System.Net.Http.Headers;

public class Order
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public int? CourierId { get; set; }
    public int CustomerId { get; set; }
    public override string ToString()
    {
        string result = 
            $"=====ЗАКАЗ=====\n" +
            $"ID заказа: №{Id}\n" +
            $"Дата создания: {CreateDate}\n" +
            $"Статус заказа: {OrderStatus}\n" +
            $"ID курьера: {CourierId}\n" +
            $"ID клиента: {CustomerId}\n" +
            $"Продукты:\n"
        ;

        foreach (var product in OrderItems)
        {
            result += product + "\n";
        }

        return result;
    }
}