using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backendMiniCore.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? EmployeeId { get; set; }
        public string Name { get; set; }

    }
}
