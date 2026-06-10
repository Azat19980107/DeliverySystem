using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
        