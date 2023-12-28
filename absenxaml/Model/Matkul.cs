using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsenMVC.Model
{
    public class Matkul
    {
        [BsonId, BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("nama")]
        public string Nama { get; set; }

        public Matkul(string nama, ObjectId id = default(ObjectId))
        {
            this.Id = id;
            this.Nama = nama;
        }
    }
}
