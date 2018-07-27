using ServiceStack.Redis;
using System;
using System.Collections.Generic;

namespace ServiceStack.Demos.Redis
{

    class Todo
    {
        public long Id { get; set; }
        public string Content { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var redisConnectionString = string.Empty;

            IRedisClientsManager clientsManager =
                new BasicRedisClientManager(redisConnectionString);

            using (IRedisClient redis = clientsManager.GetClient())
            {
                redis.IncrementValue("counter");
                List<string> days = redis.GetAllItemsFromList("seconds");

                //Access Typed API
                var redisTodos = redis.As<Todo>();

                redisTodos.Store(new Todo
                {
                    Id = redisTodos.GetNextSequence(),
                    Content = "Learn Redis",
                });

                var todo = redisTodos.GetById(1);
                foreach (var item in redisTodos.GetValues(redisTodos.GetAllKeys()))
                {
                    Console.WriteLine($"{item.Id} {item.Content}");
                    Console.WriteLine(item.ToJson());
                }

            }

            Console.WriteLine("Hello World!");
        }
    }
}
