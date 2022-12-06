using MongoDB.Driver;

namespace Soulgram.Mongo.Repository.Interfaces
{
    public interface IMongoConnection
    {
        Task<IClientSessionHandle> Session { get; }
        IMongoCollection<TDocument> GetMongoCollection<TDocument>();
    }
}
