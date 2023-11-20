using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using webapi.Models;

namespace webapi
{
    public class PasswordDBContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public PasswordDBContext()
        {
            var client = new MongoClient("mongodb://localhost:27017"); 
            _mongoDatabase = client.GetDatabase("PasswordDB");
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
