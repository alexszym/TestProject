using Api.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Api
{
    public class MongoDbContext : IMongoDbContext
    {
        private IMongoDatabase _db { get; set; }

        public MongoDbContext(IOptions<DatabaseSettingsOptions> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _db = mongoClient.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }

        public async Task<long> GetNextSequenceNumber<T>(string collectionName)
        {
            var sequenceCollection = GetCollection<SequenceNumber>(collectionName + "-sequence");

            var nextSequence = await sequenceCollection.Find(x => true).SingleOrDefaultAsync();
            if (nextSequence is null)
            {
                var collection = GetCollection<T>(collectionName);
                var count = await collection.CountDocumentsAsync(x => true);
                return count + 1;
            }
            else
            {
                return nextSequence.Value;
            }
        }

        public async Task IncrementSequenceNumberAsync<T>(string collectionName)
        {
            var sequenceCollection = GetCollection<SequenceNumber>(collectionName + "-sequence");

            var nextSequence = await sequenceCollection.Find(x => true).SingleOrDefaultAsync();
            if (nextSequence is null)
            {
                var collection = GetCollection<T>(collectionName);
                var count = await collection.CountDocumentsAsync(x => true);
                await sequenceCollection.InsertOneAsync(new SequenceNumber()
                {
                    Id = "SeqNum",
                    Value = count + 1
                });
            }
            else
            {
                await sequenceCollection.ReplaceOneAsync(x => true, new SequenceNumber()
                {
                    Id = nextSequence.Id,
                    Value = nextSequence.Value + 1
                });
            }
        }
    }
}
