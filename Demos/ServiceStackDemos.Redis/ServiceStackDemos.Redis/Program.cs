
namespace ServiceStackDemos.Redis
{
    using ServiceStack;
    using ServiceStack.Redis;
    using System;
    using System.Linq;

    class Program
    {
        class Data
        {
            public long Id { get; set; }
            public string Content { get; set; }
        }
        static void Main(string[] args)
        {
            using (var redisManager = new RedisManagerPool("localhost:6379"))
            {
                IRedisClient redis = redisManager.GetClient();

                var redisData = redis.As<Data>();
                var _data = new Data { Id = redisData.GetNextSequence(), Content = "Data" };
                redisData.Store(_data);
                //var result = redisData.GetById(_data.Id);
                //Console.WriteLine(result.ToJson());
                foreach (Data item in redisData.GetAll())
                    Console.WriteLine($"{item.ToJson()}");
            }

            Console.ReadKey();
        }
    }
}
