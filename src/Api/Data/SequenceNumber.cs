using MongoDB.Bson.Serialization.Attributes;

namespace Api.Data
{
    public class SequenceNumber
    {
        [BsonId]
        public string Id { get; set; }
        public long Value {get; set; }
    }
}
