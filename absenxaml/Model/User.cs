using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsenMVC.Model
{

    public class User
    {
        [BsonId, BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("nama")]
        public string Nama { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }

        public User(string nama, string username, string password, string role = "mahasiswa")
        {
            Username = username;
            Password = password;
            this.Nama = nama;
            this.Role = role;
        }
    }
}
