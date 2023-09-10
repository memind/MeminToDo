namespace Dashboard.Aggregator.Models.EntertainmentModels
{
    public class GameModel
    {
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string GameName { get; set; }
        public string Studio { get; set; }
        public bool IsCompleted { get; set; }
    }
}
