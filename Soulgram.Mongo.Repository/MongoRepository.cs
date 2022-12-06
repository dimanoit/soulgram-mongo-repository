using MongoDB.Driver;
using Soulgram.Mongo.Repository.Interfaces;
using System.Linq.Expressions;

namespace Soulgram.Mongo.Repository
{
    internal class MongoRepository<TDocument> where TDocument : class
    {
        private readonly IMongoConnection _connection;

        public MongoRepository(IMongoConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        protected IMongoCollection<TDocument> Collection => _connection.GetMongoCollection<TDocument>();

        public async Task<ICollection<TProjected>> FilterByAsync<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression,
            CancellationToken cancellationToken = default)
        {
            return await Collection
                .Find(filterExpression)
                .Project(projectionExpression)
                .ToListAsync(cancellationToken);
        }

        public async Task<TProjected?> FindOneAsync<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression,
            CancellationToken cancellationToken)
        {
            return await Collection
                .Find(filterExpression)
                .Project(projectionExpression)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task InsertOneAsync(TDocument document, CancellationToken cancellationToken)
        {
            var insertOptions = new InsertOneOptions
            {
                BypassDocumentValidation = false
            };

            await Collection.InsertOneAsync(document, insertOptions, cancellationToken);
        }

        public async Task InsertManyAsync(ICollection<TDocument> documents, CancellationToken cancellationToken)
        {
            var insertOptions = new InsertManyOptions
            {
                BypassDocumentValidation = false
            };

            await Collection.InsertManyAsync(documents, insertOptions, cancellationToken);
        }

        public async Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            await Collection.FindOneAndDeleteAsync(filterExpression);
        }

        public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            await Collection.DeleteManyAsync(filterExpression);
        }
    }
}
