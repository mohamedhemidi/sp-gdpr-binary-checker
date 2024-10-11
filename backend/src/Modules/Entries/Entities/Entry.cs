using MongoDB.Bson.Serialization.Attributes;
namespace Entries.Entities
{
    public class Entry
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("_userId"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string? UserId { get; set; }
        [BsonElement("string"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string? String { get; set; }
        [BsonElement("good"), BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public bool? Good { get; set; }
    }
}
