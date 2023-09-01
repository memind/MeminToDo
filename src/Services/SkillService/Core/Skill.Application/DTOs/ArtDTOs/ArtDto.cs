
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Skill.Domain.Enums;

namespace Skill.Application.DTOs.ArtDTOs
{
    public class ArtDto
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Name { get; set; }
        public Style Style { get; set; }
    }
}
