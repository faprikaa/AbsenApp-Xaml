using AbsenMVC.Model;
using Amazon.Runtime.Documents;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                new User("Anton", "mhs", new List<MatkulItem>{
                    new MatkulItem("RPL", "Senin", "07.00"), 
                    new MatkulItem("PSC", "Selasa", "09.00") 
                }),
                new User("Bayu", "mhs", new List<MatkulItem>{
                    new MatkulItem("RPL", "Senin", "07.00"), 
                    new MatkulItem("PSC", "Selasa", "09.00"),
                    new MatkulItem("PDE", "Rabu", "09.00") 
                }),
                new User("Caca", "mhs", new List<MatkulItem>{
                    new MatkulItem("RPL", "Senin", "07.00"), 
                    new MatkulItem("PSC", "Selasa", "09.00") 
                }),
                new User("Pak Dustin", "dosen", new List<MatkulItem>{
                    new MatkulItem("RPL", "Senin", "07.00"), 
                    new MatkulItem("PSC", "Selasa", "09.00") 
                }),
                new User("Bu Enoki", "dosen", new List<MatkulItem>{
                    new MatkulItem("PDE", "Rabu", "09.00") 
                }),
                new User("Mas Fatin", "admin", null),
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
    }
}
