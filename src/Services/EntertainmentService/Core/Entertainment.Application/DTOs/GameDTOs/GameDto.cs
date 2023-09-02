using Entertainment.Domain.Enums;

namespace Entertainment.Application.DTOs.GameDTOs
{
    public class GameDto
    {
        public GameDto()
        {
            Genres = new List<GameGenre>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string GameName { get; set; }
        public List<GameGenre> Genres { get; set; }
        public string Studio { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
