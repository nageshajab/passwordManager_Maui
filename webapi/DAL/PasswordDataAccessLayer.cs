using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.DAL
{
    public class PasswordDataAccessLayer
    {
        private IConfiguration _configuration;
        PasswordDBContext db;

        public PasswordDataAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new PasswordDBContext(_configuration);
        }        

        //To Get all Password details        
        public List<Password> GetAllPasswords()
        {
            try
            {
                return db.PasswordRecord.Find(_ => true).ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new Password record
        public void AddPassword(Password password)
        {
            try
            {
                db.PasswordRecord.InsertOne(password);
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular password
        public Password GetPasswordData(string id)
        {
            try
            {
                FilterDefinition<Password> filterEmployeeData = Builders<Password>.Filter.Eq("Id", id);

                return db.PasswordRecord.Find(filterEmployeeData).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particular password
        public void UpdatePassword(Password password)
        {
            try
            {
                db.PasswordRecord.ReplaceOne(filter: g => g.Id == password.Id, replacement: password);
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record of a particular password
        public void DeletePassword(string id)
        {
            try
            {
                FilterDefinition<Password> employeeData = Builders<Password>.Filter.Eq("Id", id); db.PasswordRecord.DeleteOne(employeeData);
            }
            catch
            {
                throw;
            }
        }


    }
}


