namespace Entertainment.Application.Repositories.Common
{
    public interface IDapperBaseReadRepository
    {
        T Get<T>(string command);
        Task<T> GetAsync<T>(string command);

        List<T> GetAll<T>(string command);
        Task<List<T>> GetAllAsync<T>(string command);
    }
}
