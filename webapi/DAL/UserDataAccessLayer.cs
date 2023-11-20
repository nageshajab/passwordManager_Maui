using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.DAL
{
    public class UserDataAccessLayer
    {
        PasswordDBContext db = new PasswordDBContext();

        //To Get all User details        
        public List<User> GetAllUsers()
        {
            try
            {
                return db.UserRecord.Find(_ => true).ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new User record
        public void AddUser(User User)
        {
            try
            {
                db.UserRecord.InsertOne(User);
            }
            catch
            {
                throw ;
            }
        }

        //Get the details of a particular User
        public User GetUserData(string id)
        {
            try
            {
                FilterDefinition<User> filterEmployeeData = Builders<User>.Filter.Eq("Id", id);

                return db.UserRecord.Find(filterEmployeeData).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public User GetUserData(string username,string password)
        {
            try
            {
                FilterDefinition<User> filterEmployeeData = Builders<User>.Filter.Eq("UserName", username);
                filterEmployeeData= filterEmployeeData & Builders<User>.Filter.Eq("Password1", password);

                return db.UserRecord.Find(filterEmployeeData).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particular User
        public void UpdateUser(User User)
        {
            try
            {
                db.UserRecord.ReplaceOne(filter: g => g.Id == User.Id, replacement: User);
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record of a particular User
        public void DeleteUser(string id)
        {
            try
            {
                FilterDefinition<User> employeeData = Builders<User>.Filter.Eq("Id", id); db.UserRecord.DeleteOne(employeeData);
            }
            catch
            {
                throw;
            }
        }


    }
}


