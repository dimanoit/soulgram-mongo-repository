using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Soulgram.Mongo.Repository.Interfaces;
using Soulgram.Mongo.Repository.Models;

namespace Soulgram.Mongo.Repository
{
    internal class MongoConnection : IMongoConnection
    {
        private readonly IMongoClient _mongoClient;

        public MongoConnection(
            IMongoClient mongoClient,
            IOptions<DbSettings> settings)
        {
            var dbSettings = settings ?? throw new ArgumentNullException(nameof(settings));
            _mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));

            Database = _mongoClient.GetDatabase(dbSettings.Value.DatabaseName)
                       ?? throw new ArgumentNullException(dbSettings.Value.DatabaseName);
        }

        private IMongoDatabase Database { get; }

        public Task<IClientSessionHandle> Session => _mongoClient.StartSessionAsync();

        public IMongoCollection<TDocument> GetMongoCollection<TDocument>()
        {
            var collectionName = typeof(TDocument).Name;
            return Database.GetCollection<TDocument>(collectionName);
        }
    }
}