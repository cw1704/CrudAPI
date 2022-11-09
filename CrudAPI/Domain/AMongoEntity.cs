namespace CrudAPI.Domain
{
    public class AMongoEntity
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}