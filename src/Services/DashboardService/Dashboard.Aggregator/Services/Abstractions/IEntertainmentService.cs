namespace Dashboard.Aggregator.Services.Abstractions
{
    public interface IEntertainmentService
    {
        public Task<int> GetTotalBookCount();
        public Task<int> GetTotalBookNoteCount();
        public Task<int> GetTotalGameCount();
        public Task<int> GetTotalShowCount();
        public Task<int> GetUserBookCount(string id);
        public Task<int> GetUserBookNoteCount(string id);
        public Task<int> GetUserGameCount(string id);
        public Task<int> GetUserShowCount(string id);
    }
}
