using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
