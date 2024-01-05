using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace absenxaml.Model
{
    public class MatkulUser
    {
        public MatkulUser(ObjectId matkulId = default, ObjectId userId = default, string hari = default, string jamMulai = default, string jamSelesai = default)
        {
            MatkulId = matkulId;
            Hari = hari;
            UserId = userId;
            JamMulai = jamMulai;
            JamSelesai = jamSelesai;
        }

        [BsonId, BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("matkul_id")]
        public ObjectId MatkulId { get; set; }
        
        
        [BsonElement("user_id")]
        public ObjectId UserId { get; set; }

        [BsonElement("hari")]
        public String Hari { get; set; }

        [BsonElement("jam_mulai")]
        public string JamMulai { get; set; }

        [BsonElement("jam_selesai")]
        public string JamSelesai { get; set; }



    }
}
