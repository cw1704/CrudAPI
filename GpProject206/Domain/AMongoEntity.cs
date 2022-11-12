using System;

namespace GpProject206.Domain
{
    public class AMongoEntity
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public DateTime CreationDT { get; private set; }

        public AMongoEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreationDT = DateTime.Now;
        }
    }
}