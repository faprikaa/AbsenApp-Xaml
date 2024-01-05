using absenxaml.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsenMVC.Model
{
    public class DbManager
    {
        public static MongoClient client;
        public static IMongoDatabase db;


        static DbManager()
        {
            client = new MongoClient();
            db = client.GetDatabase("absen");
            bool isConnected = db.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1500);
            if (!isConnected)
            {
                client = new MongoClient("mongodb://absen:123@167.172.92.130:27017/?authSource=absen");
                db = client.GetDatabase("absen");
                DeleteAllDocument(true);
            }
            DeleteAllDocument(true);


        }

        public static IMongoCollection<Matkul> getTbMatkul()
        {
            var cltName = "matkul";
            if (CheckCollection(cltName))
            {
                db.CreateCollection(cltName);
            }
            var matkuls = db.GetCollection<Matkul>(cltName);
            return matkuls;
        }
        public static IMongoCollection<User> getTbUser()
        {
            var cltName = "user";
            if (CheckCollection(cltName))
            {
                db.CreateCollection(cltName);
            }
            var users = db.GetCollection<User>(cltName);
            return users;
        }
        
        public static IMongoCollection<MatkulUser> getTbMatkulUser()
        {
            var cltName = "matkul_user";
            if (CheckCollection(cltName))
            {
                db.CreateCollection(cltName);
            }
            var matkulUser = db.GetCollection<MatkulUser>(cltName);
            return matkulUser;
        }
        
        public static IMongoCollection<Absensi> getTbAbsensi()
        {
            var cltName = "absensi";
            if (!CheckCollection(cltName))
            {
                db.CreateCollection(cltName);
            }
            var absensi = db.GetCollection<Absensi>(cltName);
            return absensi;
        }
        private static bool CheckCollection(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collectionCursor = db.ListCollections(new ListCollectionsOptions { Filter = filter });
            return collectionCursor.Any();
        }

        private static void DeleteAllDocument(bool insertData = false)
        {
            var m = db.GetCollection<Matkul>("matkul");
            m.DeleteMany(FilterDefinition<Matkul>.Empty);

            var u = db.GetCollection<User>("user");
            u.DeleteMany(FilterDefinition<User>.Empty);

            var mu = db.GetCollection<MatkulUser>("matkul_user");
            mu.DeleteMany(FilterDefinition<MatkulUser>.Empty);
            
            var a = db.GetCollection<Absensi>("absensi");
            a.DeleteMany(FilterDefinition<Absensi>.Empty);
            if (insertData)
            {
                InsertData();
            }

        }

        private static void InsertData()
        {
            string folderFileJson = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\FileJson");
            List<String> listFileJsonName = new List<String>
            {
                "absen.matkul.json",
                "absen.matkul_user.json",
                "absen.user.json",
                "absen.absensi.json",
            };

            listFileJsonName.ForEach(fileName =>
            {
                string pathJson = Path.Combine(folderFileJson, fileName);
                string json = File.ReadAllText(pathJson);
                if (fileName == "absen.matkul.json")
                {
                    var listData = BsonSerializer.Deserialize<List<Matkul>>(json);
                    var coll = db.GetCollection<Matkul>("matkul");
                    coll.InsertMany(listData);
                }
                else if (fileName == "absen.matkul_user.json")
                {
                    var listData = BsonSerializer.Deserialize<List<MatkulUser>>(json);
                    var coll = db.GetCollection<MatkulUser>("matkul_user");
                    coll.InsertMany(listData);
                }
                else if (fileName == "absen.user.json")
                {
                    var listData = BsonSerializer.Deserialize<List<User>>(json);
                    var coll = db.GetCollection<User>("user");
                    coll.InsertMany(listData);
                }
                else if (fileName == "absen.absensi.json")
                {
                    var listData = BsonSerializer.Deserialize<List<Absensi>>(json);
                    var coll = db.GetCollection<Absensi>("absensi");
                    coll.InsertMany(listData);
                }

            });
        }
    }
}
