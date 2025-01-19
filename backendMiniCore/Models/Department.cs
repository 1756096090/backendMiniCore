using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backendMiniCore.Models
{
    public class Department
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DepartmentId { get; set;}
        public string Name { get; set;}
    }
}
