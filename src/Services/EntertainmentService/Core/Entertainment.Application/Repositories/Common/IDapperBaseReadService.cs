namespace Entertainment.Application.Repositories.Common
{
    public interface IDapperBaseReadService
    {
        T Get<T>(string command, object parms);
        Task<T> GetAsync<T>(string command, object parms);

        List<T> GetAll<T>(string command, object parms);
        Task<List<T>> GetAllAsync<T>(string command, object parms);
    }
}
