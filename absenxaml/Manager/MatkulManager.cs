using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace AbsenMVC.Model
{
    public class MatkulManager
    {
        private IMongoCollection<Matkul> _matkul;

        public MatkulManager()
        {
            _matkul = DbManager.getTbMatkul();
        }

        public IMongoCollection<Matkul> getMatkul()
        {
            var c = _matkul.CountDocuments(FilterDefinition<Matkul>.Empty);
            if (c < 1)
            {
                insetDataIfNull();
            }
            return _matkul;
        }

        private void insetDataIfNull()
        {
            List<Matkul> matkuls = new List<Matkul>
            {
                new Matkul("RPL", ObjectId.Parse("657b08352e39af52b5c8db53")),
                new Matkul("PSC", ObjectId.Parse("6579b32c973b0428dc6fbb98")),
                new Matkul("PDE", ObjectId.Parse("6579b355c67f3103b02078c7")),
            };
            _matkul.InsertMany(matkuls);

        }

        public void UpdateMatkul(FilterDefinition<Matkul> filter, UpdateDefinition<Matkul> update)
        {
            _matkul.UpdateOne(filter, update);
        }

        public bool InsertNewMatkul(Matkul newMatkul)
        {
            var find = _matkul.Find(m => m.Nama == newMatkul.Nama).Any();
            Debug.WriteLine(find);
            if (find)
            {
                return false;
            }
            else
            {
                _matkul.InsertOne(newMatkul);
                return true;
            }
        }

        public void DeleteMatkul(ObjectId objId)
        {
            var filter = Builders<Matkul>.Filter.Eq(_ => _.Id, objId);
            _matkul.DeleteOne(filter);
        }

        public Matkul GetMatkulByName(string name)
        {
            var filter = Builders<Matkul>.Filter.Eq(m => m.Nama, name);
            var result = _matkul.Find(filter).FirstOrDefault();
            return result;
        }
    }
}
