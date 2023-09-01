using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Skill.Domain.Entities.Common
{
    public class SkillBase
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
