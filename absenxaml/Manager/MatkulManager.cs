using MongoDB.Driver;
using System;
using System.Collections.Generic;

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
            if (_matkul == null)
            {
                insetDataIfNull();
            }
            insetDataIfNull();
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
            Matkul matkul = new Matkul("RPL");
            var filter = Builders<Matkul>.Filter.Eq(m => m.Nama, "RPL");
            var query = Builders<Matkul>.Update.Set(r => r.Nama, "RPL");
            try
            {
                _matkul.UpdateOne(filter, query, new UpdateOptions { IsUpsert = true });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateMatkul(FilterDefinition<Matkul> filter, UpdateDefinition<Matkul> update)
        {
            _matkul.UpdateOne(filter, update);
        }
    }
}
