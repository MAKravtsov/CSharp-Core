using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shed.CoreKit.WebApi;

namespace Interfaces.Interfaces
{
    public interface IActivityLogger
    {
        [HttpGet, Route("get/{timestamp}")]
        IEnumerable<LogEvent> Get(long timestamp);
    }
}
