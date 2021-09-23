using Interfaces.Models;
using Shed.CoreKit.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Interfaces
{
    public interface IShoppingCart
    {
        internal Cart Get();

        [HttpPut, Route("addorder/{productId}/{qty}")]
        Cart AddOrder(Guid productId, int qty);

        Cart DeleteOrder(Guid orderId);

        [Route("getevents/{timestamp}")]
        IEnumerable<CartEvent> GetCartEvents(long timestamp);
    }
}
