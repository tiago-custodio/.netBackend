using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotnetBackend.Database
{
    public class MongoDBService
    {
        private readonly IMongoDatabase _database;

        public MongoDBService(IOptions<DatabaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.connectionString);
            _database = client.GetDatabase(databaseSettings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName) =>
            _database.GetCollection<T>(collectionName);
    }
}