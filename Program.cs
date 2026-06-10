using System.Diagnostics;
using System.Xml;
using DeliverySystem.Models;

DeliveryService service = new DeliveryService ();

while (true)
{
    Console.WriteLine("Выберите пользователя\n" + "Клиент - 1\n" + "Курьер - 2");
    int userNumber = int.Parse(Console.ReadLine());

    switch(userNumber)
    {
        case 1:
            {
                service.CreateCustomer();
            }
        break;

    }
    break;
}