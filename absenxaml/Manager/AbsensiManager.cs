using AbsenMVC.Model;
using absenxaml.Model;
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
    public class AbsensiManager
    {
        public IMongoCollection<Absensi> _absensi;
        public AbsensiManager()
        {
            _absensi = DbManager.getTbAbsensi();
        }

        public IMongoCollection<Absensi> getAbsensi()
        {
            return _absensi;
        }
        public List<BsonDocument> getAbsensiWithAggregate()
        {
            var agg = _absensi.Aggregate()
                .Lookup(
                    "matkul_user",
                    "matkul_user_id",
                    "_id",
                    "matkul_user"
                )
                .Unwind("matkul_user")
                .Lookup(
                    "matkul",
                    "matkul_user.matkul_id",
                    "_id",
                    "matkul"
                )
                .Unwind("matkul")
                .Lookup(
                    "user",
                    "matkul_user.user_id",
                    "_id",
                    "user"
                )
              .Unwind("user");
            return agg.ToList();
        }

        public void UpdateAbsensi(ObjectId absensiId, string newValueAbsen)
        {
            var filter = Builders<Absensi>.Filter.Where(a => a.AbsenId == absensiId);
            Console.WriteLine(")))" + _absensi.Find(filter).FirstOrDefault().ToJson());
            var query = Builders<Absensi>.Update.Set(a => a.Absen, newValueAbsen);
            _absensi.UpdateOne(filter, query);
            Console.WriteLine(")))2" + _absensi.Find(filter).FirstOrDefault().ToJson());

        }

        public void InsertIfNotExist(MatkulUser matkulUser)
        {
            var currentDate = DateTime.Today;
            var x = _absensi.Find(a => a.MatkulUserId == matkulUser.Id && a.Tanggal == DateTime.Today).FirstOrDefault();
            Console.WriteLine(DateTime.Today);
            Console.WriteLine(x.ToJson());

            if (x == null)
            {
                _absensi.InsertOne(
                    new Absensi(
                        matkulUser.Id,
                        currentDate,
                        "Absen"
                    ));
            }
        }

        public Absensi GetAbsensiByMatkulUserId(ObjectId MUId)
        {
            return _absensi.Find(a => a .MatkulUserId == MUId).FirstOrDefault();
        }
    }
}
