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
                new User("Anton", "mahasiswa", new List<MatkulItem>{
                    new MatkulItem(ObjectId.Parse("657b08352e39af52b5c8db53"), "Senin", "07:00", "08:00"), 
                    new MatkulItem(ObjectId.Parse("6579b32c973b0428dc6fbb98"), "Selasa", "09.00", "10:00") 
                }),
                new User("Bayu", "mahasiswa", new List<MatkulItem>{
                    new MatkulItem(ObjectId.Parse("657b08352e39af52b5c8db53"), "Senin", "07.00", "08:00"),
                    new MatkulItem(ObjectId.Parse("6579b32c973b0428dc6fbb98"), "Selasa", "09.00", "10:00"),
                    new MatkulItem(ObjectId.Parse("6579b355c67f3103b02078c7"), "Rabu", "09.00", "10:00") 
                }),
                new User("Caca", "mahasiswa", new List<MatkulItem>{
                    new MatkulItem(ObjectId.Parse("657b08352e39af52b5c8db53"), "Senin", "07.00", "08:00"),
                    new MatkulItem(ObjectId.Parse("6579b32c973b0428dc6fbb98"), "Selasa", "09.00", "10:00") 
                }),
                new User("Pak Dustin", "dosen", new List<MatkulItem>{
                    new MatkulItem(ObjectId.Parse("657b08352e39af52b5c8db53"), "Senin", "07.00", "08:00"),
                    new MatkulItem(ObjectId.Parse("6579b32c973b0428dc6fbb98"), "Selasa", "09.00", "10:00") 
                }),
                new User("Bu Enoki", "dosen", new List<MatkulItem>{
                    new MatkulItem(ObjectId.Parse("6579b355c67f3103b02078c7"), "Rabu", "09.00", "10:00") 
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

        public List<BsonDocument> GetMatkulByUserId(ObjectId userId)
        {
            var query = _users.Aggregate().Match(u => u.Id == userId).Lookup("matkul",  "matkul.matkul_id", "_id", "dataMatkul").ToList();
            return query;
        }
    }
}
