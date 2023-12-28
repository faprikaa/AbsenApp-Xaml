using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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
                DeleteAllDocument();
            }
        }

        public static IMongoCollection<Matkul> getTbMatkul()
        {
            var cltName = "matkul";
            if (CheckCollection(cltName))
            {
                var matkuls = db.GetCollection<Matkul>(cltName);
                return matkuls;
            }
            db.CreateCollection(cltName);
            var matkuls2 = db.GetCollection<Matkul>(cltName);
            return matkuls2;
        }
        public static IMongoCollection<User> getTbUser()
        {
            var cltName = "user";
            if (CheckCollection(cltName))
            {
                var users = db.GetCollection<User>(cltName);
                return users;
            }
            db.CreateCollection(cltName);
            var users2 = db.GetCollection<User>(cltName);
            return users2;
        }
        private static bool CheckCollection(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collectionCursor = db.ListCollections(new ListCollectionsOptions { Filter = filter });
            return collectionCursor.Any();
        }

        private static void DeleteAllDocument()
        {
            var mat = "matkul";
            var m = db.GetCollection<Matkul>(mat);
            m.DeleteMany(FilterDefinition<Matkul>.Empty);
            
            var us = "user";
            var u = db.GetCollection<User>(us);
            u.DeleteMany(FilterDefinition<User>.Empty);

        }
    }
}
