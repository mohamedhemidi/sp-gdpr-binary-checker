
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
namespace Entries.Data
{
    public class MongoDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration.GetSection("MongoSettings");
            var mongoUrl = MongoUrl.Create(_configuration["MongoSettings:ConnectionString"]);
            var mongoClient = new MongoClient(mongoUrl);
            _database = mongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);
        }

        public IMongoDatabase? Database => _database;
    }
}
