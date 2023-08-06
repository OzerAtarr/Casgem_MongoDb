using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductID { get; set; }=string.Empty;
        [BsonElement("rooms")]
        public int Rooms { get; set; }
        [BsonElement("bathroom")]
        public int Bathroom { get; set; }
        [BsonElement("adress")]
        public string Adress { get; set; } = string.Empty;
        [BsonElement("area")]
        public int Area { get; set; }
        [BsonElement("price")]
        public decimal Price { get; set; }
        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string SallerId { get; set; }
    }
}
