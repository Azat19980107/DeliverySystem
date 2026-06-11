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

        case 3:
            {
                while (true)
                {
                    Console.WriteLine
                    (
                        "Выберите команду:\n" +
                        "Посмотреть всех клиентов - 1\n" +
                        "Посмотреть всех курьеров - 1\n"
                    );

                    int command = int.Parse(Console.ReadLine());

                    switch(command)
                    {
                        case 1:
                            {
                                service.ShowAllCustomers();
                            }
                        break;
                    }

                    break;
                }
            }
        break;

    }
    
}