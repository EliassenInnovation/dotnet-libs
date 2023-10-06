using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nucleus.Lesson.Persistence.Collections
{
    [BsonIgnoreExtraElements]
    public class TeacherCollection
    {
        [BsonElement("userId")]
        public string? UserId { get; set; }
        [BsonElement("fullName")]
        public string? FullName { get; set; }
        [BsonElement("emailAddress")]
        public string? EmailAddress { get; set; }
    }
}
