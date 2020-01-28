namespace Clientes.Infraestructure.MongoDb
{
    public class MongoDbDatabaseSettings : IMongoDbDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDbDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
