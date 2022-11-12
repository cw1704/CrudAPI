using GpProject206.Domain;
using GpProject206.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpProject206.Services
{
    public class OrderService : AMongoCollectionProvider<Order>
    {
        public OrderService(IDatabaseSettings settings) : base(settings, "orders")
        {
        }

        public async Task<IList<Order>> ReadByPromotions(IList<string> p)
        {
            var filterDef = new FilterDefinitionBuilder<Order>();
            var filter = filterDef.In(x => x.PromotionCode, p);
            return await _collection.FindAsync(filter).Result.ToListAsync();
        }

    }
}
