﻿using System;
using StackExchange.Redis;

namespace Redis
{
    public class Repository
    {
        protected readonly IDatabase RedisRepo;
        private readonly ConnectionMultiplexer _connection;

        public Repository(int num)
        { 
            _connection = ConnectionMultiplexer.Connect("Redis");
            RedisRepo = _connection.GetDatabase(num);
        }

        ~Repository()
        {
            _connection?.Dispose();
        }
    }
}