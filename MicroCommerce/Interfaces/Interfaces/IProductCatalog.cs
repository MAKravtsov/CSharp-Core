using Interfaces.Models;
using Shed.CoreKit.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Interfaces
{
    public interface IProductCatalog
    {
        IEnumerable<Product> Get();

        [Route("get/{productId}")]
        public Product Get(int productId);
    }
}
