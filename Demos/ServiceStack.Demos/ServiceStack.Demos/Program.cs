using ServiceStack.OrmLite;
using System;

namespace ServiceStack.Demos
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbFactory = new OrmLiteConnectionFactory(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=ShoppingDB;Integrated Security=true",
                SqlServerDialect.Provider);

            using (var db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<Products>();


                var result = db.LoadSelect<Products>();
                Console.WriteLine(result.ToJson());
            }



            Console.WriteLine("Hello World!");
            Console.ReadKey(); 
        }
    }
}
