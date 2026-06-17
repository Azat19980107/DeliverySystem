using System.Buffers;
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
                service.CustomerMenu();
            }
        break;

        case 2:
            {
                service.CourierMenu(); 
            }
        break;

        case 3:
            {
                service.AdminMenu();
            
            }
        break;

    }
    
}