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
            var c = _users.CountDocuments(FilterDefinition<User>.Empty);
            if (c < 1)
            {
                insetDataIfNull();
            }
            return _users;
        }

        private void insetDataIfNull()
        {
            List<User> users = new List<User>
            {
                new User( "Anton", "mahasiswa",ObjectId.Parse("658fa5f7a358110c2d0d6ca4") ),
                new User("Bayu", "mahasiswa", ObjectId.Parse("658fa5f7a358110c2d0d6ca5")),
                new User("Caca", "mahasiswa", ObjectId.Parse("658fa5f7a358110c2d0d6ca6")),
                new User("Pak Dustin", "dosen", ObjectId.Parse("658fa5f7a358110c2d0d6ca7")),
                new User("Bu Enoki", "dosen", ObjectId.Parse("658fa5f7a358110c2d0d6ca8")),
                new User("Mas Fatin", "admin", ObjectId.Parse("658fa5f7a358110c2d0d6ca9")),
            };
            _users.InsertMany(users);
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
    }
}
