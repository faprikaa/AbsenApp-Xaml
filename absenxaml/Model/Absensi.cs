using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace absenxaml.Model
{
    public class Absensi
    {
        public Absensi(ObjectId matkulUserId, DateTime tanggal, string absen)
        {
            MatkulUserId = matkulUserId;
            Tanggal = tanggal;
            Absen = absen;
        }

        [BsonId, BsonElement("absen_id")]
        public ObjectId AbsenId { get; set; }

        [BsonElement("matkul_user_id")]
        public ObjectId MatkulUserId { get; set; }

        [BsonElement("tanggal"), BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Tanggal { get; set; }

        [BsonElement("absen")]
        public string Absen {  get; set; }



    }
}
