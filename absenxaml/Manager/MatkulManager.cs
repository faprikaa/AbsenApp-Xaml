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
            return _matkul;
        }

        public void UpdateMatkul(FilterDefinition<Matkul> filter, UpdateDefinition<Matkul> update)
        {
            _matkul.UpdateOne(filter, update);
        }

        public bool InsertNewMatkul(Matkul newMatkul)
        {
            var find = _matkul.Find(m => m.Nama == newMatkul.Nama).Any();
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
        
        public Matkul GetMatkulById(ObjectId matkulId)
        {
            var filter = Builders<Matkul>.Filter.Eq(m => m.Id, matkulId);
            var result = _matkul.Find(filter).FirstOrDefault();
            return result;
        }

    }
}
