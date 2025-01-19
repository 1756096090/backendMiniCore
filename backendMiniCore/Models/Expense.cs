using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backendMiniCore.Models
{
    public class Expense
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ExpenseID { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set; }

    }
}
