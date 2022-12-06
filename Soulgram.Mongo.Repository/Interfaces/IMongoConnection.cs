using MongoDB.Driver;

namespace Soulgram.Mongo.Repository.Interfaces
{
    internal interface IMongoConnection
    {
        Task<IClientSessionHandle> Session { get; }
        IMongoCollection<TDocument> GetMongoCollection<TDocument>();
    }
}
