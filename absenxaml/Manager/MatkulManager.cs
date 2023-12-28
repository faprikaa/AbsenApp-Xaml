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
                new Matkul("RPL"),
                new Matkul("PSC"),
                new Matkul("PDE"),
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
    }
}
