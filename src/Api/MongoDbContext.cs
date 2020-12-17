using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api
{
    public class MongoDbContext : IMongoDbContext
    {
            private IMongoDatabase _db { get; set; }
            private MongoClient _mongoClient { get; set; }
            public IClientSessionHandle Session { get; set; }
            public MongoDbContext(IOptions<DatabaseSettingsOptions> options)
            {
                _mongoClient = new MongoClient(options.Value.ConnectionString);
                _db = _mongoClient.GetDatabase(options.Value.DatabaseName);
            }

            public IMongoCollection<T> GetCollection<T>(string name)
            {
                return _db.GetCollection<T>(name);
            }
    }
}
