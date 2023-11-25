using MongoDB.Driver;
using webapi.Models;

namespace webapi
{
    public class AuthenticationDBContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IConfiguration _configuration;

        public AuthenticationDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
                        
            var client = new MongoClient(_configuration.GetConnectionString("conn1")); 
            _mongoDatabase = client.GetDatabase(_configuration.GetSection("databaseName").Value);
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
