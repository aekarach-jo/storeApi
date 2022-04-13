using StoreBK.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace StoreBK.Services
{
    public class StoreService
    {
        public readonly IMongoCollection<Store> _stores;
        public StoreService(DatabaseSetting settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            var filter = Builders<Store>.Filter.Where(settings=> settings.Status == "Open");
            _stores = database.GetCollection<Store>(settings.StoreCollection);
        }

        public List<Store> GetAllStoreForApi() => _stores.Find(store => true).ToList();
        public List<Store> GetAllStore() => _stores.Find(store => store.Status == "Open").ToList(); 
        public Store GetStoreById(string storeId) => _stores.Find<Store>(store => store.StoreId == storeId).FirstOrDefault();

        public Store CreateStore(Store store)
        {
            _stores.InsertOne(store);
            return store;
        }
        public void EditStore(string storeId, Store storeBody) => _stores.ReplaceOne(stores => stores.StoreId == storeId, storeBody);
        public void DeleteStore(string storeId, Store storeBody) => _stores.ReplaceOne(stores => stores.StoreId == storeId, storeBody);
    }
}
