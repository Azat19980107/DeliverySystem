using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace DeliverySystem.Models
{
    public class DeliveryService
    {
        List<Customer> customers = new ();
        List<Courier> couriers = new ();
        List<Order> orders = new();
        private int orderId = 1000;
        public void CreateCustomer ()
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine ();

            Console.WriteLine("Придумайте ID");
            int id = int.Parse(Console.ReadLine());

            customers.Add(new Customer
            {
                Id = id,
                Name = name
            });

            Console.WriteLine("Аккаунт создан");
        }
        public void ShowAllCustomers()
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }
        public void CreateOrder ()
        {
            Order order = new ()
            {
                OrderId = orderId++,
                CreateDate = DateTime.Now,
                OrderStatus = OrderStatus.Created,

            };

            Console.WriteLine("Что хотите заказать?");

            Product product = new ()
            {
                ProductName = Console.ReadLine()
            };

            Console.WriteLine("Выберите кол-во");

            OrderItem orderItem = new ()
            {
                Product = product,
                Quantity = int.Parse(Console.ReadLine())
            };

            order.OrderItems.Add(orderItem);

            orders.Add(order);
        }
        public void ShowOrders ()
        {
            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }
        }

        public void CreateCourier()
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();

            Console.WriteLine("Придумайте ID");
            int id = int.Parse(Console.ReadLine());

            couriers.Add(new Courier
            {
                Name = name,
                Id = id
            });

            Console.WriteLine("Аккаунт создан");
        }
        public void ShowAllCouriers ()
        {
            foreach (var courier in couriers)
            {
                Console.WriteLine(courier);
            }
        }
        public List<Order> GetNoCourierOrder(DeliveryService service)
        {
            var noCourierOrders = service.orders.Where(order => order.CourierId == 0).ToList();

            return noCourierOrders;
        }

        public void ShowNoCourierOrders (List<Order> orders)
        {
            foreach (var order in orders)
            {
                Console.WriteLine(order.OrderId);
            }
        }

        public void AcceptOrder(int orderId, int courierId)
        {
            var pickedOrder = orders.First(order => order.OrderId == orderId);
            pickedOrder.CourierId = courierId;
            pickedOrder.OrderStatus = OrderStatus.Accepted;
        }
        
    }
}
        