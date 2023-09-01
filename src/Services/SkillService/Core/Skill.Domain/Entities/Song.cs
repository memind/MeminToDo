using Skill.Domain.Entities.Common;
using Skill.Domain.Enums;

namespace Skill.Domain.Entities
{
    public class Song : SkillBase
    {
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public Instrument Instrument{ get; set; }
    }
}
