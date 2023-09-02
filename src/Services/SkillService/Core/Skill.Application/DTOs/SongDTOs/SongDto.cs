using MongoDB.Bson;
using Skill.Domain.Enums;

namespace Skill.Application.DTOs.SongDTOs
{
    public class SongDto
    {
        public ObjectId Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public Instrument Instrument { get; set; }
    }
}
