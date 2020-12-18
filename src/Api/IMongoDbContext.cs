using MongoDB.Driver;
using System.Threading.Tasks;

namespace Api
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);

        Task IncrementSequenceNumberAsync<T>(string collectionName);

        Task<long> GetNextSequenceNumber<T>(string collectionName);
    }
}
