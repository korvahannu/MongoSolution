

using MongoDB.Bson.Serialization.Attributes;

namespace MongoDataAccess.Schemas
{
    public class PersonSchema : ISchema
    {
        public string Table { get; } = "Person";
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public Guid GetId()
        {
            return Id;
        }
    }
}
