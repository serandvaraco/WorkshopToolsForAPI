namespace ServiceStackDemos.ORM
{
    /*
     * Install-Package ServiceStack.OrmLite.SqlServer
     */
    using ServiceStack;
    using ServiceStack.OrmLite;
    using System;
    using System.Linq;

    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }

    public class Program
    {
        static void Main(string[] args)
        {
            var dbfactory =
                new OrmLiteConnectionFactory(@"Data Source=(localdb)\.;
                            Initial Catalog=ShoppingDB;Integrated Security=true",
                            SqlServerDialect.Provider);

            using (var db = dbfactory.Open())
            {
                if (db.CreateTableIfNotExists<Products>())
                {
                    db.Insert<Products>(new Products
                    {
                        Id = 100,
                        Name = "Product POCO Service Stack",
                        Description = "Service Stack SQL Provider",
                        Price = 0M
                    });
                }

                

                var results = db.LoadSelect<Products>()
                    .SafeWhere(x => x.Price > 0);

                Console.WriteLine(results.Select(x=>new { x.Name, x.Price }).ToCsv());
            }

            Console.ReadKey();
        }
    }
}
