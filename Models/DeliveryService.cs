using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using DeliverySystem.Data;

namespace DeliverySystem.Models
{
    public class DeliveryService
    {
        List<Customer> customers = new ();
        List<Courier> couriers = new ();
        List<Order> orders = new();
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
        public void ShowNoCourierOrders ()
        {
            var noCourierOrders = orders.Where(order => order.CourierId == 0);
            
            foreach (var order in noCourierOrders)
            {
                Console.WriteLine(order.OrderId);
            }
        }
        private int orderId = 1000;
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
            return couriers.FirstOrDefault(courier => courier.Id == id);
        }
        public int ReadId ()
        {
            //Console.WriteLine("Введите ID");
            int id = int.Parse(Console.ReadLine());
            return id;
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
            Order order = new ()
            {
                OrderId = orderId++,
                CreateDate = DateTime.Now,
                OrderStatus = OrderStatus.Created,
                CustomerId = customer.Id,

            };

            while (true)
            {

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

                //orders.Add(order);

                Console.WriteLine
                (
                    "Добавить еще - 1\n" +
                    "Оформить - 0\n"
                );

                int continutOrNot = int.Parse(Console.ReadLine());

                switch (continutOrNot)
                {
                    case 0:
                            orders.Add(order);
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
            var foundOrders = orders.Where(order => order.CustomerId == id).ToList();

            foreach (var order in foundOrders)
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
        public void AcceptOrder(int orderId, int courierId)
        {
            var pickedOrder = orders.First(order => order.OrderId == orderId);

            if (pickedOrder.CourierId == 0)
            {
                pickedOrder.CourierId = courierId;
                pickedOrder.OrderStatus = OrderStatus.Accepted;
                Console.WriteLine("Заказ принят");
            }else
            {
                Console.WriteLine("Заказ у другого курьера");
            }
        }
        public void ShowCourierOrders (int courierId)
        {
            var foundOrders = orders.Where(order => order.CourierId == courierId);

            foreach (var order in foundOrders)
            {
                Console.WriteLine($"ID: {order.OrderId}, Статус: {order.OrderStatus}");
            }
        }
        public void DeliverOrder(int orderId, int courierId)
        {
            var pickedOrder = orders.FirstOrDefault(order => order.OrderId == orderId && order.CourierId == courierId);

            if(pickedOrder != null)
            {
                pickedOrder.OrderStatus = OrderStatus.Delivered;
                Console.WriteLine("Заказ доставлен");
            }else
            {
                Console.WriteLine("Заказ не ваш");
            }
        }
        
    }
}
        