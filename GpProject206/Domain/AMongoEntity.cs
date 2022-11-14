using System;

namespace GpProject206.Domain
{
    public class AMongoEntity
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; protected set; }
        public DateTime CreationDT { get; private set; }
        public void SetId(string id) => this.Id = id;

        public AMongoEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreationDT = DateTime.Now;
        }
    }
}