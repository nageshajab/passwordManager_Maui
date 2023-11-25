using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using webapi.Models;

namespace webapi
{
    public class AuthenticationDBContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public AuthenticationDBContext()
        {
            var client = new MongoClient("mongodb://localhost:27017"); 
            _mongoDatabase = client.GetDatabase("AuthenticationDB");
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
