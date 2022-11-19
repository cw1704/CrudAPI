using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GpProject206.Domain;
using GpProject206.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GpProject206.Services
{
    public abstract class AMongoCollectionProvider<T> where T : AMongoEntity
    {
        protected readonly IMongoCollection<T> _collection;

        public AMongoCollectionProvider(IDatabaseSettings settings, string collectionName)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.Database);
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<T> Create(T item) //Create
        {
            await _collection.InsertOneAsync(item);
            return item;
        }

        public async Task<IList<T>> ReadAll() 
        {
            return await _collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
        public async Task<IList<T>> ReadListed(IList<string> p) 
        {
            var filterDef = new FilterDefinitionBuilder<T>();
            var filter = filterDef.In(x => x.Id, p);
            return await _collection.FindAsync(filter).Result.ToListAsync();
        }
        public async Task<T> ReadId(string id)
        {
            return await _collection.FindAsync(t => t.Id == id).Result.FirstOrDefaultAsync();
        }
        
        public async Task<T> ReadKey(string key, string value)
        {
            var filter = Builders<T>.Filter.Eq(key, value);
            return await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();
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

        public async Task<bool> IsExist(string id)
        {
            return await _collection.CountDocumentsAsync(x => x.Id == id) > 0;
        }

        public async Task<bool> IsExist(string key, string value)
        {
            var filter = Builders<T>.Filter.Eq(key, value);
            return await _collection.FindAsync(filter).Result.FirstOrDefaultAsync() == null;
        }


    }
}
