using Entertainment.Domain.Entities.Common;

namespace Entertainment.Domain.Entities
{
    public class Game : EntertainmentBase
    {
        public string GameName { get; set; }
        public string Studio { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
