using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace absenxaml.Model
{
    public class Kelas
    {

        [BsonId, BsonElement("kelas_id")]
        public ObjectId KelasId { get; set; }

        [BsonElement("list_matkul_user")]
        public List<MatkulUser> ListMatkulUser { get; set; }

        [BsonElement("hari")]
        public String Hari { get; set; }

        [BsonElement("jam_mulai")]
        public string JamMulai { get; set; }

        [BsonElement("jam_selesai")]
        public string JamSelesai { get; set; }

    }
}
