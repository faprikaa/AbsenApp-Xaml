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
