using MongoDB.Driver;
using webapi.Models;

namespace webapi.DAL
{
    public class UserDataAccessLayer
    {
        AuthenticationDBContext db;
        private IConfiguration _configuration;

        public UserDataAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new AuthenticationDBContext(_configuration);
        }

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
                throw;
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
        public bool UserExists(string id)
        {
            try
            {
                FilterDefinition<User> filterEmployeeData = Builders<User>.Filter.Eq(u=>u.Id, id);

                return db.UserRecord.Find(filterEmployeeData).Any();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get user details by user name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUserByUserName(string username)
        {
            try
            {
                FilterDefinition<User> filterEmployeeData = Builders<User>.Filter.Eq("username", username);

                return db.UserRecord.Find(filterEmployeeData).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public User GetUserByUserEmail(string email)
        {
            try
            {
                FilterDefinition<User> filterEmployeeData = Builders<User>.Filter.Eq("email", email);

                return db.UserRecord.Find(filterEmployeeData).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular User
        public User GetUserData(string username, string password)
        {
            var userfilter = Builders<User>.Filter;
            try
            {
                FilterDefinition<User> usernameOrEmailFilter= userfilter.Or(
                  userfilter.Where(u => u.UserName == username), userfilter.Where(u => u.Email == username));

                FilterDefinition<User> passwordFilter = 
                  userfilter.Where(u => u.Password1== password);

                FilterDefinition <User> filter = userfilter.And(usernameOrEmailFilter,passwordFilter);

                var user = db.UserRecord.Find(filter).FirstOrDefault();
                return user;
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


