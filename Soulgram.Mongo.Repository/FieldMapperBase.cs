using Soulgram.Mongo.Repository.Interfaces;

namespace Soulgram.Mongo.Repository
{
    public abstract class FieldMapperBase
    {
        public void MapModels()
        {
            var mappers = GetModelMappers().ToList();

            mappers.ForEach(m => m.MapFields());
        }

        public abstract IEnumerable<IModelMapper> GetModelMappers();
    }
}
