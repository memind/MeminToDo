namespace Dashboard.Aggregator.Services.Abstractions
{
    public interface ISkillService
    {
        Task<int> GetTotalArtCount();
        Task<int> GetTotalSongCount();
        Task<int> GetUsersArtCount(string id);
        Task<int> GetUsersSongCount(string id);

    }
}
