using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GpProject206.Domain
{
    public class ProductCategory : AMongoEntity
    {
        public string Name { get; set; }
    }
}