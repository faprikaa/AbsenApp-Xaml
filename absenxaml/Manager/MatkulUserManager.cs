using AbsenMVC.Model;
using absenxaml.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace absenxaml.Manager
{

    public class MatkulUserManager
    {
        private IMongoCollection<MatkulUser> _matkulUser;
        private FilterDefinitionBuilder<MatkulUser> filterBuilder = Builders<MatkulUser>.Filter;
        private UpdateDefinitionBuilder<MatkulUser> updateBuilder = Builders<MatkulUser>.Update;

        public MatkulUserManager()
        {
            _matkulUser = DbManager.getTbMatkulUser();
        }

        public List<BsonDocument> getMatkulUserByUserId(ObjectId userId)
        {
            var c = _matkulUser.CountDocuments(FilterDefinition<MatkulUser>.Empty);
            if (c < 1)
            {
                insertDataIfNull();
            }
            var filter = filterBuilder.Eq(mu => mu.UserId, userId);
            var result = _matkulUser.Find(filter).ToList();
            var agg = _matkulUser.Aggregate()
                .Match(mu => mu.UserId == userId)
                .Lookup(
                    "matkul",
                    "matkul_id",
                    "_id",
                    "DataMatkul"
                )
                .Unwind("DataMatkul");
            return agg.ToList();

        }


        private void insertDataIfNull()
        {
            List<MatkulUser> matkulUsers = new List<MatkulUser>
            {
                // user anton
                new MatkulUser(ObjectId.Parse("657b08352e39af52b5c8db53"), ObjectId.Parse("658fa5f7a358110c2d0d6ca4"), "Senin", "07.00", "08:00"),
                new MatkulUser(ObjectId.Parse("6579b32c973b0428dc6fbb98"), ObjectId.Parse("658fa5f7a358110c2d0d6ca4"), "Selasa", "09.00", "10:00"),
                new MatkulUser(ObjectId.Parse("6579b355c67f3103b02078c7"), ObjectId.Parse("658fa5f7a358110c2d0d6ca4"), "Rabu", "07.00", "08:00"),
                
                // user Bayu
                new MatkulUser(ObjectId.Parse("657b08352e39af52b5c8db53"), ObjectId.Parse("658fa5f7a358110c2d0d6ca5"), "Senin", "07.00", "08:00"),
                new MatkulUser(ObjectId.Parse("6579b32c973b0428dc6fbb98"), ObjectId.Parse("658fa5f7a358110c2d0d6ca5"), "Selasa", "09.00", "10:00"),
                new MatkulUser(ObjectId.Parse("6579b355c67f3103b02078c7"), ObjectId.Parse("658fa5f7a358110c2d0d6ca5"), "Rabu", "09.00", "10:00"),
                
                // user Bayu
                new MatkulUser(ObjectId.Parse("657b08352e39af52b5c8db53"), ObjectId.Parse("658fa5f7a358110c2d0d6ca5"), "Jumat", "13.00", "15:00"),
                new MatkulUser(ObjectId.Parse("6579b32c973b0428dc6fbb98"), ObjectId.Parse("658fa5f7a358110c2d0d6ca5"), "Selasa", "09.00", "10:00"),
                new MatkulUser(ObjectId.Parse("6579b355c67f3103b02078c7"), ObjectId.Parse("658fa5f7a358110c2d0d6ca5"), "Rabu", "09.00", "10:00"),

                // user pak Dustin 
                new MatkulUser(ObjectId.Parse("657b08352e39af52b5c8db53"), ObjectId.Parse("658fa5f7a358110c2d0d6ca7"),"Senin", "07.00", "08:00"),
                new MatkulUser(ObjectId.Parse("6579b32c973b0428dc6fbb98"), ObjectId.Parse("658fa5f7a358110c2d0d6ca7"), "Selasa", "09.00", "10:00"),

                // user bu enoki
                new MatkulUser(ObjectId.Parse("6579b355c67f3103b02078c7"), ObjectId.Parse("658fa5f7a358110c2d0d6ca8"),"Rabu", "09.00", "10:00")

            };
            _matkulUser.InsertMany(matkulUsers);
        }

        public void InsertNewMatkulUser(MatkulUser matkulUser)
        {
            _matkulUser.InsertOne(matkulUser);
        }

        public void UpdateById(ObjectId matkulUserId, MatkulUser newMatkuluser) {
            var filter = Builders<MatkulUser>.Filter.Where(mu => mu.Id == matkulUserId);
            var update = Builders<MatkulUser>.Update
                .Set(mu => mu.MatkulId, newMatkuluser.MatkulId)
                .Set(mu => mu.Hari, newMatkuluser.Hari)
                .Set(mu => mu.JamMulai, newMatkuluser.JamMulai)
                .Set(mu => mu.JamSelesai, newMatkuluser.JamSelesai);
            _matkulUser.FindOneAndUpdate(filter, update);
        }

        public void DeleteById(ObjectId matkulUserId)
        {
            _matkulUser.FindOneAndDelete(mu => mu.Id == matkulUserId);
        }

    }
}
