using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using DeliverySystem.Data;
using Microsoft.EntityFrameworkCore;

namespace DeliverySystem.Models
{
    public class DeliveryService
    {
        public void CustomerMenu ()
        {
            Console.WriteLine("Для входа введите ID");
            int customerId = ReadId();
            var foundAccount = FindAccount (customerId);

            if (foundAccount != null)
            {
                while (true)
                {
                    Console.WriteLine
                        (
                            "Оформить заказ - 1\n" +
                            "Посмотреть историю заказов - 2\n" +
                            "Главное меню - 0\n"
                        );

                    int commands = int.Parse(Console.ReadLine());

                    switch (commands)
                    {
                        case 1:
                            {
                                CreateOrder(foundAccount);
                            }
                            break;
                        case 2:
                            {
                                ShowMyOrders(customerId);
                            }
                            break;
                        case 0:
                            {
                                return;
                            }
                        default:
                            {
                                continue;
                            }
                    }
                }
            }else
            {
                Console.WriteLine("Аккаунта с таким ID нет");
            }
        }
        public void CourierMenu ()
        {
            Console.WriteLine("Введите ID для входа");
            int courierId = ReadId();
            var foundCourier = FindCourier(courierId);

            if (foundCourier != null)
            {
                while (true)
                {
                    Console.WriteLine
                    (
                        "Посмотреть свободные заказы - 1\n" +
                        "Принять заказ - 2\n" + 
                        "Посмотреть мои заказы - 3\n" +
                        "Отметить как доставленный - 4\n" +
                        "Главное меню - 0"
                    );

                    int commands = int.Parse(Console.ReadLine());

                    switch (commands)
                    {
                        case 1:
                            {
                                ShowNoCourierOrders();
                            }
                        break;
                        case 2:
                            {
                                Console.WriteLine("Введите ID заказа");
                                int orderId = int.Parse(Console.ReadLine());
                                AcceptOrder(orderId, foundCourier.Id);
                            }
                        break;
                        case 3:
                            {
                                ShowCourierOrders(foundCourier.Id);
                            }
                        break;
                        case 4:
                            {
                                Console.WriteLine("Введите ID заказа, чтобы отметить как доставленный");
                                int orderId = int.Parse(Console.ReadLine());
                                DeliverOrder(orderId, foundCourier.Id);
                            }
                        break;
                        case 0:
                            {
                                return;
                            }
                    }
                }
            }
            
        }
        public void AdminMenu()
        {
            while (true)
                {
                    Console.WriteLine
                    (
                        "Выберите команду:\n" +
                        "Посмотреть всех клиентов - 1\n" +
                        "Посмотреть всех курьеров - 2\n" +
                        "Создать клиентский аккаунт - 3\n" +
                        "Cоздать курьерский аккаунт - 4\n" +
                        "Главное меню - 0\n"
                    );

                    int command = int.Parse(Console.ReadLine());

                    switch(command)
                    {
                        case 1:
                            {
                                ShowAllCustomers();
                            }
                        break;

                        case 2:
                            {
                                ShowAllCouriers();
                            }
                        break;
                        case 3:
                            {
                                CreateCustomer();
                            }
                        break;
                        case 4:
                            {
                                CreateCourier();
                            }
                        break;
                        case 0:
                        {
                            return;
                        }
                    }
            }
        }
        public void ShowNoCourierOrders()
        {
            using var context = new AppDbContext();

            var foundOrders = context.Orders.Where(order => order.OrderStatus == OrderStatus.Created);

            foreach (var order in foundOrders)
            {
                Console.WriteLine($"Заказ: №{order.Id}");
            }
        }
        public void CreateCustomer ()
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine ();

            Console.WriteLine("Придумайте ID");
            int id = int.Parse(Console.ReadLine());

            using var context = new AppDbContext();
            var customer = new Customer
            {
                Name = name,
                Id = id
            };

            context.Customers.Add(customer);
            context.SaveChanges();

            Console.WriteLine("Аккаунт создан");
        }
        public Customer FindAccount (int id)
        {
            using var context = new AppDbContext();

            var foundCustomer = context.Customers.FirstOrDefault(customer =>  customer.Id == id);

            if (foundCustomer != null)
            {
                Console.WriteLine($"Добро пожаловать {foundCustomer.Name}");
            }

            return foundCustomer;   
        }
        public Courier FindCourier (int id)
        {
            using  var context = new AppDbContext ();
            var foundCourier = context.Couriers.FirstOrDefault(courier =>  courier.Id == id);

            if (foundCourier != null)
            {
                Console.WriteLine($"Здравствуй, {foundCourier.Name}");
                return foundCourier;
            }

            return null;
        }
        public int ReadId ()
        {
           while (true)
            {
                if(int.TryParse(Console.ReadLine(), out int id))
                {
                    return id;
                }

                Console.WriteLine("ID должен быть числом");
            }
        }
        public void ShowAllCustomers()
        {
            using var context = new AppDbContext();
            var customers = context.Customers.ToList();
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }
        public void CreateOrder (Customer customer)
        {
            using var context = new AppDbContext();

            var order = new Order()
            {
 
               CreateDate = DateTime.Now,
               OrderStatus = OrderStatus.Created,
               CustomerId = customer.Id,

            };

            while (true)
            {
                OrderItem orderItem = new();

                Console.WriteLine("Что хотите заказать?");

                orderItem.ProductName = Console.ReadLine();

                Console.WriteLine("Выберите кол-во");

                orderItem.Quantity = int.Parse(Console.ReadLine());

                order.OrderItems.Add(orderItem);

                Console.WriteLine
                (
                    "Добавить еще - 1\n" +
                    "Оформить - 0\n"
                );

                int continutOrNot = int.Parse(Console.ReadLine());

                switch (continutOrNot)
                {
                    case 0:
                        context.Orders.Add(order);
                        context.SaveChanges();
                        Console.WriteLine("Заказ оформлен!");
                        return;

                    case 1:

                        continue;
                    default:

                        continue;

                }
            }
        }
        public void ShowMyOrders (int id)
        {
            using var context = new AppDbContext();

            var orders = context.Orders
                .Include(order => order.OrderItems)
                .Where(order => order.CustomerId ==  id)
                .ToList();

            foreach(var order in orders)
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

            using var context = new AppDbContext();
            var courier = new Courier
            {
                Id = id,
                Name = name
            };

            context.Couriers.Add(courier);
            context.SaveChanges();

            Console.WriteLine("Аккаунт создан");
        }
        public void ShowAllCouriers ()
        {
            using var context = new AppDbContext();
            var couriers = context.Couriers.ToList();
            foreach (var courier in couriers)
            {
                Console.WriteLine(courier);
            }
        }
        public void AcceptOrder(int orderId, int courierId)
        {
            using var context = new AppDbContext();

            var pickedOrder = context.Orders.FirstOrDefault(order => order.Id == orderId);

            if (pickedOrder.CourierId == null)
            {
                pickedOrder.CourierId = courierId;
                pickedOrder.OrderStatus = OrderStatus.Accepted;
                context.SaveChanges ();
                Console.WriteLine("Заказ принят");
            }
            else
            {
                Console.WriteLine("Заказ у другого курьера");
            }
        }
        public void ShowCourierOrders (int courierId)
        {
            using var context = new AppDbContext();

            var foundOrders = context.Orders.Where(order => order.CourierId == courierId).ToList();

            foreach (var order in foundOrders)
            {
                Console.WriteLine($"ID: {order.Id}, Статус: {order.OrderStatus}");
            }
        }
        public void DeliverOrder(int orderId, int courierId)
        {
            using var context = new AppDbContext();
         
            var pickedOrder = context.Orders.FirstOrDefault(order => order.Id == orderId && order.CourierId == courierId);

            if(pickedOrder != null)
            {
                pickedOrder.OrderStatus = OrderStatus.Delivered;
                context.SaveChanges ();
                Console.WriteLine("Заказ доставлен");
            }else
            {
                Console.WriteLine("Заказ не ваш");
            }
        }
        
    }
}
        