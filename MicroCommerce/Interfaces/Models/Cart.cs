using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Models
{
    public class Cart
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
