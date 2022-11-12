using GpProject206.Domain;
using GpProject206.Settings;

namespace GpProject206.Services
{
    public class PromotionService : AMongoCollectionProvider<Promotion>
    {
        public PromotionService(IDatabaseSettings settings) : base(settings, "promotions") {}
    }
}
