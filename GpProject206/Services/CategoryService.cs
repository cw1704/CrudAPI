using GpProject206.Domain;
using GpProject206.Settings;

namespace GpProject206.Services
{
    public class CategoryService : AMongoCollectionProvider<ProductCategory>
    {
        public CategoryService(IDatabaseSettings settings) : base(settings, "categories")
        {
        }
    }
}
