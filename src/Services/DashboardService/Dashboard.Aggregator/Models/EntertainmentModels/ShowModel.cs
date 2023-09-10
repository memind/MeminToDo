namespace Dashboard.Aggregator.Models.EntertainmentModels
{
    public class ShowModel
    {
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string ShowName { get; set; }
        public int EpisodeCount { get; set; }
        public bool IsFinished { get; set; }
    }
}
