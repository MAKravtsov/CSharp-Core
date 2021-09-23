using Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Models
{
    public class CartEvent : EventBase
    {
        public CartEventTypeEnum Type { get; set; }
        public Order Order { get; set; }
    }
}
