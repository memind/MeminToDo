using Skill.Domain.Entities.Common;
using Skill.Domain.Enums;

namespace Skill.Domain.Entities
{
    public class Art : SkillBase
    {
        public string Name { get; set; }
        public Style Style{ get; set; }
    }
}
