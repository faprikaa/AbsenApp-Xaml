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

    }
}
