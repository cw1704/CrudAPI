using System.Collections.Generic;
using System.Threading.Tasks;
using CrudAPI.Domain;
using CrudAPI.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CrudAPI.Services
{
    public class MenuService
    {
        private readonly IMongoCollection<ProductItem> _products;
        private readonly IMongoCollection<ProductCategory> _cats;
        private readonly IMongoCollection<Promotion> _promotion;

        public MenuService(IDatabaseSettings settings)
        {
            var mongo_setting = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(mongo_setting);
            var database = client.GetDatabase(settings.Database);
            _products = database.GetCollection<ProductItem>("products");
            _cats = database.GetCollection<ProductCategory>("categories");
            _promotion = database.GetCollection<Promotion>("promotions");
        }

        // ====================================================================

        public async Task<ProductItem> InsertProduct(ProductItem item) //Create
        {
            await _products.InsertOneAsync(item);
            return item;
        }
        public async Task<ProductCategory> InsertCategory(ProductCategory item) //Create
        {
            await _cats.InsertOneAsync(item);
            return item;
        }

        // ====================================================================

        public async Task<IList<ProductItem>> GetFullMenu() //Read
        {
            return await _products.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
        public async Task<IList<ProductCategory>> GetCategories() //Read
        {
            return await _cats.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<ProductItem> GetById(string itemId) //Read
        {
            return await _products.FindAsync(t => t.Id == itemId).Result.FirstOrDefaultAsync();
        }
    }
}
