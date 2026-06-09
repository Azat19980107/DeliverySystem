using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliverySystem.Models
{
    public class DeliveryService
    {
        public Customer CreateCustomer ()
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine ();
            Console.WriteLine("Придумайте ID");
            int id = int.Parse(Console.ReadLine());

            return new Customer
            {
                Id = id,
                Name = name
            };
        }
        public Courier CreateCourier ()
        {
            return new Courier
            {

            };
        }
    }
}
