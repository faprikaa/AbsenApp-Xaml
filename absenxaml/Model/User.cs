using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsenMVC.Model
{
    public class MatkulItem
    {
        public MatkulItem(ObjectId matkulId, string hari, string jamMulai, string jamSelesai)
        {
            MatkulId = matkulId;
            Hari = hari;
            JamMulai = jamMulai;
            JamSelesai = jamSelesai;
        }

        [BsonElement("matkul_id"), BsonIgnoreIfNull]
        public ObjectId MatkulId { get; set; }
        
        [BsonElement("hari")]
        public string Hari { get; set; }
        
        [BsonElement("jam_mulai")]
        public string JamMulai { get; set; }
        [BsonElement("jam_selesai")]
        public string JamSelesai { get; set; }

    }

    public class User
    {
        [BsonId, BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("nama")]
        public string Nama { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }

        [BsonElement("matkul")]
        public List<MatkulItem> Matkul { get; set; }

        public User(string nama, string role = "mahasiswa", List<MatkulItem> matkul = null)
        {
            this.Nama = nama;
            this.Role = role;
            this.Matkul = matkul ?? new List<MatkulItem>();
        }
    }
}
