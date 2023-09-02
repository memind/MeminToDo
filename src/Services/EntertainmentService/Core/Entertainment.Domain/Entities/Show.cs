using Entertainment.Domain.Entities.Common;

namespace Entertainment.Domain.Entities
{
    public class Show : EntertainmentBase
    {
        public string ShowName { get; set; }
        public int EpisodeCount { get; set; }
        public bool IsFinished { get; set; } = false;
    }
}
