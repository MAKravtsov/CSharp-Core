using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Product Clone()
        {
            return new Product {
                Id = Id,
                Name = Name
            };
        }
    }
}
