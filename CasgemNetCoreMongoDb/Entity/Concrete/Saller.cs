using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Saller
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SallerId { get; set; } = string.Empty;
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;
        [BsonElement("surname")]
        public string Surname { get; set; } = string.Empty;
        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;
        [BsonElement("phone")]
        public string Phone { get; set; } = string.Empty;
        [BsonElement("imageurl")]
        public string ImageUrl { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public List<Product>? Products { get; set; }
    }
}
