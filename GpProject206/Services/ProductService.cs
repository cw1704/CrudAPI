using GpProject206.Domain;
using GpProject206.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpProject206.Services
{
    public class ProductService : AMongoCollectionProvider<Product>
    {
        /*private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<ProductCategory> _cats;

        public ProductService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.Database);
            _products = database.GetCollection<Product>("products");
            _cats = database.GetCollection<ProductCategory>("categories");
        }

        // ====================================================================

        public async Task<Product> CreateProduct(Product item) //Create
        {
            await _products.InsertOneAsync(item);
            return item;
        }
        public async Task<ProductCategory> CreateCategory(ProductCategory item) //Create
        {
            await _cats.InsertOneAsync(item);
            return item;
        }

        // ====================================================================

        public async Task<IList<Product>> ReadFullMenu() //Read
        {
            return await _products.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
        public async Task<IList<ProductCategory>> ReadAllCategories() //Read
        {
            return await _cats.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
        public async Task<Product> ReadProductById(string itemId) //Read
        {
            return await _products.FindAsync(t => t.Id == itemId).Result.FirstOrDefaultAsync();
        }
        public async Task<bool> IsCatExist(string id)
        {
            return await _cats.CountDocumentsAsync(x => x.Id == id) > 0;
        }*/
        public ProductService(IDatabaseSettings settings) : base(settings, "products")
        {
        }
    }
}
