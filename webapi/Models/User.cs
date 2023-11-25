using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace webapi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName{ get; set; }
        public string Email{ get; set; }
        public string Password1 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
