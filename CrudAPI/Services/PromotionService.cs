using CrudAPI.Domain;
using CrudAPI.Settings;

namespace CrudAPI.Services
{
    public class PromotionService : AMongoCollection<Promotion>
    {
        public PromotionService(IDatabaseSettings settings) : base(settings, "promotions") {}
    }
}
