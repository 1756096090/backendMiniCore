using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace backendMiniCore.Config
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;
        public MongoDBContext(IOptions<MongoDBSettings> settings)
        {
            try
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                _database = client.GetDatabase(settings.Value.DatabaseName);

            }
            catch(Exception ex) {
                throw new Exception("No se pudo conectar a MongoDB", ex);

            }

        }

        public IMongoDatabase GetDatabase() { return _database; }
    }
}
