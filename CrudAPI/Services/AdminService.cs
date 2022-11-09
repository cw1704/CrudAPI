using CrudAPI.Domain;
using CrudAPI.Settings;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace CrudAPI.Services
{
    public class AdminService
    {
        private readonly IMongoCollection<ProductItem> _products;
        private readonly IMongoCollection<ProductCategory> _cats;

        public AdminService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.Database);
            _products = database.GetCollection<ProductItem>("products");
        }


        // ====================================================================

        public async Task<ProductItem> Update(ProductItem item) //Update
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
