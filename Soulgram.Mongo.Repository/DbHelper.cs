using MongoDB.Driver;
using System.Linq.Expressions;

namespace Soulgram.Mongo.Repository
{
    public static class DbHelper
    {
        public static async Task CreateUniqueIndex<T>(
        IMongoDatabase db,
        string collectionName,
        Expression<Func<T, object>> field)
        {
            var collection = db.GetCollection<T>(collectionName);
            var uniqueKeyName = Builders<T>.IndexKeys.Ascending(field);
            var indexModel = new CreateIndexModel<T>(uniqueKeyName, new CreateIndexOptions
            {
                Unique = true
            });

            await collection.Indexes.CreateOneAsync(indexModel);

        }
    }
}
