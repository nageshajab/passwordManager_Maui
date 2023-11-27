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
        string encryptionkey = "key must be long";

        public PasswordDataAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new PasswordDBContext(_configuration);

        }        

        //To Get all Password details        
        public List<Password> GetAllPasswords(string userid)
        {

            List<Password> lst;
            List<Password> returnval = new List<Password>();
            try
            {
                //lst= db.PasswordRecord.Find(_ => true).ToList();
                lst= db.PasswordRecord.Find(p=>p.UserId==userid).ToList();
                foreach (Password p in lst)
                {
                    p.Password1 = AesOperation.DecryptString(encryptionkey, p.Password1);

                    returnval.Add(p);
                }
            }
            catch
            {
                throw;
            }
                        
            return returnval;
        }

        //To Add new Password record
        public  async Task AddPassword(Password password)
        {
            try
            {
                password.Password1=AesOperation.EncryptString(encryptionkey, password.Password1);
               await db.PasswordRecord.InsertOneAsync(password);
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular password
        public Password GetPasswordData(Password password)
        {
            try
            {
                //and both
                FilterDefinition<Password> filterEmployeeData = Builders<Password>.Filter.And(Builders<Password>.Filter.Eq(p=>p.Id, password.Id),
                    Builders<Password>.Filter.Eq(p=>p.UserId, password.UserId));

                var p= db.PasswordRecord.Find(filterEmployeeData).FirstOrDefault();

                p.Password1=AesOperation.DecryptString(encryptionkey,p.Password1);
                return p;
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
                password.Password1 = AesOperation.EncryptString(encryptionkey, password.Password1);
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


