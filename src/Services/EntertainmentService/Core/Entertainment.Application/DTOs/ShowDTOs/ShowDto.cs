namespace Entertainment.Application.DTOs.ShowDTOs
{
    public class ShowDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string ShowName { get; set; }
        public int EpisodeCount { get; set; }
        public bool IsFinished { get; set; } = false;
    }
}
