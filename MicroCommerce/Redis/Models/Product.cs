﻿using Newtonsoft.Json;
using StackExchange.Redis;

namespace Redis.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public HashEntry HashEntry
        {
            get 
            {
                var name = new RedisValue(Id.ToString());
                var value = new RedisValue(JsonConvert.SerializeObject(this));

                var hashEntry = new HashEntry(name, value);
                return hashEntry;
            }
        }
    }
}
