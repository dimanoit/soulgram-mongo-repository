namespace Soulgram.Mongo.Repository.Models
{
    internal class DbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;
    }
}
