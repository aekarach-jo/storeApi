using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StoreBK.Models
{
    public class DatabaseSetting : IDatabaseSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string StoreCollection { get; set; }
    }
    public interface IDatabaseSetting
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string StoreCollection { get; set; }
    }
}