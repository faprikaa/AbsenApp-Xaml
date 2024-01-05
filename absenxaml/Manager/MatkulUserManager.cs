using AbsenMVC.Model;
using absenxaml.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

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

        public void UpdateById(ObjectId matkulUserId, MatkulUser newMatkuluser)
        {
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

        public List<BsonDocument> GetMahasiswaByMatkulId(ObjectId matkulId)
        {
            var hariIni = Utils.GetCurrentLocalDay();
            var agg = _matkulUser.Aggregate()
                .Match(mu => mu.MatkulId == matkulId)
                .Match(mu => mu.Hari == hariIni)
                .Lookup(
                    "matkul",
                    "matkul_id",
                    "_id",
                    "matkul"
                )
                .Unwind("matkul")
                .Lookup(
                    "user",
                    "user_id",
                    "_id",
                    "user"
                )
                .Unwind("user")
                .Lookup(
                    "absensi",
                    "_id",
                    "matkul_user_id",
                    "absensi"
                )
                .Unwind("absensi")
                .ToList();
            var mahasiswaResults = agg
                .Where(result =>
                {
                    BsonDocument user = result["user"].AsBsonDocument;
                    return user["role"].AsString == "mahasiswa";
                })
                .ToList();
            return mahasiswaResults;
        }

        public ObjectId GetCurrentMatkulId(ObjectId userId)
        {
            DateTime currentDate = DateTime.Now;
            string hariIni = Utils.GetCurrentLocalDay();
            TimeSpan currentTime = new TimeSpan(currentDate.Hour, currentDate.Minute, 0);

            var filter = Builders<MatkulUser>.Filter.Eq(mu => mu.UserId, userId) &
                         Builders<MatkulUser>.Filter.Eq(mu => mu.Hari, hariIni);
            var result = _matkulUser.Find(filter).ToList();
            var jadi = ObjectId.Empty;

            result.ForEach(mu =>
            {
                if (Utils.StringToTimeSpan(mu.JamMulai) < currentTime &&
                Utils.StringToTimeSpan(mu.JamSelesai) > currentTime) {
                    jadi = mu.MatkulId;
                }
            });
            return jadi;


        }

        public List<BsonDocument> GetHistoryByUserId(ObjectId userId)
        {
            var agg = _matkulUser.Aggregate()
                .Match(mu => mu.UserId == userId)
                .Lookup(
                "absensi",
                "_id",
                "matkul_user_id",
                "absensi")
                .Unwind("absensi");
            return agg.ToList();

        }
        public List<BsonDocument> GetJadwalByUserId(ObjectId userId)
        {
            var agg = _matkulUser.Aggregate()
                .Match(mu => mu.UserId == userId)
                .Lookup(
                    "matkul",
                    "matkul_id",
                    "_id",
                    "matkul"
                )
                .Unwind("matkul")
                .ToList();
            return agg;
        }
    }
}
