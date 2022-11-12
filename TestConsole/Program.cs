using GpProject206.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace TestConsole
{
    internal class Program
    {

        static void Main(string[] args)
        {
            IMongoCollection<Product> _products;
            IMongoCollection<ProductCategory> _cats;

            try
            {

                Console.WriteLine("Hello, World!");
                var mongo_setting = MongoClientSettings.FromConnectionString("mongodb+srv://206gp2pizza:206gp2pizza@206gp2pizza.j16dnyu.mongodb.net/?retryWrites=true&w=majority");
                var client = new MongoClient(mongo_setting);
                var database = client.GetDatabase("CrudAPI");
                _products = database.GetCollection<Product>("products");
                _cats = database.GetCollection<ProductCategory>("categories");

                Console.WriteLine(_products.FindAsync(new BsonDocument()).Result.ToListAsync().Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }
    }
}