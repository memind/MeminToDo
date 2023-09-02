using Entertainment.Domain.Entities.Common;
using Entertainment.Domain.Enums;

namespace Entertainment.Domain.Entities
{
    public class Game : EntertainmentBase
    {
        public Game()
        {
            Genres = new List<GameGenre>();
        }

        public string GameName { get; set; }
        public List<GameGenre> Genres { get; set; }
        public string Studio { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
