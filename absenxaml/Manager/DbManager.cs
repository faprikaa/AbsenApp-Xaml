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
        public static MongoClient client = new MongoClient();
        public static IMongoDatabase db = client.GetDatabase("absen");
        public static IMongoCollection<Matkul> getTbMatkul()
        {
            var matkuls  = db.GetCollection<Matkul>("matkul");
            return matkuls;
        }
    }
}
