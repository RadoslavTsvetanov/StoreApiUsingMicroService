using MongoDB.Driver;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories
{
    public class Repository
    {
        private const string CollectionName = "items";
        private readonly IMongoCollection<Item> DBcollection;

        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;


        public Repository()
        {
            var MongoClient = new MongoClient("mongodb://localhost:27017");
            var database = MongoClient.GetDatabase("Catalog");
            DBcollection = database.GetCollection<Item>(CollectionName);

        }

        public async Task<IReadOnlyCollection<Item>> GetAllAsync()
        {
            return await DBcollection.Find(filterBuilder.Empty).ToListAsync();
        }
        public async Task<Item> GetAsync(Guid id)
        {

            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.id, id);

            return await DBcollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            await DBcollection.InsertOneAsync(item);
        }

        public async Task UpdateAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.id, item.id);
            await DBcollection.ReplaceOneAsync(filter, item);
        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.id, id);
            await DBcollection.DeleteOneAsync(filter);
        }
    }
}