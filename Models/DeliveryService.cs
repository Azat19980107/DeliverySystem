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
        public void CustomerMenu (DeliveryService service)
        {
            Console.WriteLine("Для входа введите ID");
            int customerId = service.ReadId();
            var foundAccount = service.FindAccount (customerId);

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
                                service.CreateOrder(foundAccount);
                            }
                            break;
                        case 2:
                            {
                                service.ShowMyOrders(customerId);
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
        public void CourierMenu (DeliveryService service)
        {
            while (true)
            {
                Console.WriteLine("Введите ID для входа");
                int courierId = ReadId();
                var foundCourier = FindCourier(courierId);

                if (foundCourier != null)
                {
                    Console.WriteLine($"Здравствуй!{foundCourier.Name}\n");

                    Console.WriteLine
                        (
                            "Посмотреть заказы - 1\n" +
                            "Принять заказ - 2\n" 
                        );

                    int commands = int.Parse(Console.ReadLine());

                    switch (commands)
                    {

                    }
                }


            }
        }
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
        public Customer FindAccount (int id)
        {
            var foundCustomer = customers.FirstOrDefault(customer => customer.Id == id);

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

                Console.WriteLine("Оформить заказ или продолжить?");

                int continutOrNot = int.Parse(Console.ReadLine());

                switch (continutOrNot)
                {
                    case 0:
                            orders.Add(order);
                            return;
                        
                    case 1:
                        
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
        