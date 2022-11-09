using System.Collections.Generic;
using System.Threading.Tasks;
using CrudAPI.Domain;
using CrudAPI.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CrudAPI.Services
{
    public abstract class AMongoCollection<T> where T : AMongoEntity
    {
        private readonly IMongoCollection<T> _collection;

        public AMongoCollection(IDatabaseSettings settings, string collectionName)
        {
            var mongo_setting = MongoClientSettings.FromConnectionString(settings.ConnectionString);
            var client = new MongoClient(mongo_setting);
            var database = client.GetDatabase(settings.Database);
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<T> Create(T item) //Create
        {
            await _collection.InsertOneAsync(item);
            return item;
        }

        public async Task<IList<T>> Read() 
        {
            return await _collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<T> Update(T item) 
        {
            await _collection.ReplaceOneAsync(t => t.Id == item.Id, item);
            return item;
        }

        public async Task Remove(string itemId) //Delete
        {
            await _collection.DeleteOneAsync(t => t.Id == itemId);
        }
    }
}
