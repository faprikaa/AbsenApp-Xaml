using AbsenMVC.Model;
using Amazon.Runtime.Documents;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace absenxaml.Manager
{

    public class UserManager
    {
        private IMongoCollection<User> _users;

        public UserManager()
        {
            _users = DbManager.getTbUser();

        }

        public IMongoCollection<User> getUser()
        {
            return _users;
        }

        public void InsertNewUser(User user)
        {
            _users.InsertOne(user);
        }

        public void UpdateUser(FilterDefinition<User> filter, UpdateDefinition<User> update)
        {
            _users.UpdateOne(filter, update);
        }

        public void DeleteUser(ObjectId objId)
        {
            var filter = Builders<User>.Filter.Eq(_ => _.Id, objId);
            _users.DeleteOne(filter);
        }

        public User GetUserById(ObjectId id)
        {
            return _users.Find(u => u.Id == id).FirstOrDefault();
        }

        public string LoginAttempt(string username, string password)
        {
            User user = _users.Find(u => u.Username == username && u.Password == password).FirstOrDefault();
            if ( user == null)
            {
                return "unknown";
            } else
            {
                return user.Role;
            }

        }  
    }
}
