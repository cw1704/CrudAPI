using GpProject206.Domain;
using GpProject206.Settings;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace GpProject206.Services
{
    public class AdminService
    {
        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<ProductCategory> _cats;

        public AdminService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.Database);
            _products = database.GetCollection<Product>("products");
        }


        // ====================================================================

        public async Task<Product> Update(Product item) //Update
        {
            await _products.ReplaceOneAsync(t => t.Id == item.Id, item);
            return item;
        }

        // ====================================================================

        public async Task Remove(string itemId) //Delete
        {
            await _products.DeleteOneAsync(t => t.Id == itemId);
        }
    }
}
