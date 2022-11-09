using CrudAPI.Domain;
using CrudAPI.Settings;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace CrudAPI.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.Database);
            _orders = database.GetCollection<Order>("orders");
        }
        public async Task<Order> MakeOrder(Order data)
        {
            await _orders.InsertOneAsync(data);
            return data;
        }

    }
}
