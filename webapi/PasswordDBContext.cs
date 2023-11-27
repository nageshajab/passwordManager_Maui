using MongoDB.Driver;
using webapi.Models;

namespace webapi
{
    public class PasswordDBContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IConfiguration _configuration;

        public PasswordDBContext(IConfiguration configuration)
        {
            _configuration = configuration;

            var client = new MongoClient(_configuration.GetSection("ConnectionString").Value);
            _mongoDatabase = client.GetDatabase(_configuration.GetSection("databaseName").Value);
        }

        public IMongoCollection<Password> PasswordRecord
        {
            get
            {
                return _mongoDatabase.GetCollection<Password>("PasswordRecord");
            }
        }

        public IMongoCollection<User> UserRecord
        {
            get
            {
                return _mongoDatabase.GetCollection<User>("UserRecord");
            }
        }
    }
}
