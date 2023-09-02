using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Skill.Domain.Entities.Common
{
    public class SkillBase
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
