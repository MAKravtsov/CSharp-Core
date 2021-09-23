using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public Order Clone()
        {
            return new Order {
                Id = Id,
                Product = Product.Clone(),
                Quantity = Quantity

            };
        }
    }
}
