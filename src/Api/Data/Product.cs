using MongoDB.Bson.Serialization.Attributes;

namespace Api.Data
{
    public class Product
    {
        [BsonId]
        public long Id { get; set; }
        [BsonElement("price")]
        public string Price { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
