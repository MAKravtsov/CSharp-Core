using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Models
{
    public abstract class EventBase
    {
        private static long TimestampBase;

        static EventBase()
        {
            TimestampBase = new DateTime(2000, 1, 1).Ticks;
        }

        public long Timestamp { get; set; }

        public DateTime Time { get; set; }

        public EventBase()
        {
            Time = DateTime.Now;
            Timestamp = Time.Ticks - TimestampBase;
        }
    }
}
