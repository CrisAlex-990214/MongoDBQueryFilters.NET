using MongoDB.Bson.Serialization.Attributes;

namespace CleanArchitecture.Domain.Collections
{
    public class Product
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string[] Colors { get; set; }
        public double Price { get; set; }
    }
}
