using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StoreBK.Models
{
    public class Store
    {
        [BsonId]
        public string StoreId { get; set; }
        public string StoreName { get; set; }
        public string Description { get; set; }
        public string Telephone { get; set; }
    }
}