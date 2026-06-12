using System.Diagnostics;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using DeliverySystem.Models;

DeliveryService service = new DeliveryService ();

while (true)
{
    Console.WriteLine
    (
        "ВЫберите пользователя:\n" +
        "Клиент - 1\n" +
        "Курьер - 2\n" +
        "Админ - 3\n"
    );

    int userNumber = int.Parse(Console.ReadLine());

    switch(userNumber)
    {
        case 1:
            {
                Console.WriteLine
                (
                    "Cоздать аккаунт - 1\n" +
                    "Оформить заказ - 2\n" +
                    "Посмотреть историю заказов - 3\n"
                );

                int command = int.Parse(Console.ReadLine());
                
                switch(command)
                {
                    case 1:
                        {
                            service.CreateCustomer();
                        }
                    break;
                    case 2:
                        {
                            service.CreateOrder();
                        }
                    break;
                    case 3:
                        {
                            service.ShowOrders();
                        }
                    break;
                }
            }
        break;

        case 2:
            {
                Console.WriteLine
                (
                    "Cоздать аккаунт - 1\n" +
                    "Посмотреть заказы - 2\n" +
                    "Принять заказ - 3"
                );

                int command = int.Parse(Console.ReadLine());

                switch (command)
                {
                    case 1:
                        {
                            service.CreateCourier();
                        }
                    break;

                    case 3:
                        {
                            var order = service.ShowNoCourierOrder(service);
                            Console.WriteLine("Введите ID");
                            int courierId = int.Parse(Console.ReadLine());
                            var courier = service.GetCourier(service, courierId);
                            service.AcceptOrder(order, courier);
                        }
                    break;
                }
            }
        break;

        case 3:
            {
                while (true)
                {
                    Console.WriteLine
                    (
                        "Выберите команду:\n" +
                        "Посмотреть всех клиентов - 1\n" +
                        "Посмотреть всех курьеров - 2\n"
                    );

                    int command = int.Parse(Console.ReadLine());

                    switch(command)
                    {
                        case 1:
                            {
                                service.ShowAllCustomers();
                            }
                        break;

                        case 2:
                            {
                                service.ShowAllCouriers();
                            }
                        break;
                    }

                    break;
                }
            }
        break;

    }
    
}