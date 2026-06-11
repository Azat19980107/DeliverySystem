using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliverySystem.Models
{
    public class OrderItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return
                $"{Product.ProductName} x{Quantity}"; 
        }
    }
}
