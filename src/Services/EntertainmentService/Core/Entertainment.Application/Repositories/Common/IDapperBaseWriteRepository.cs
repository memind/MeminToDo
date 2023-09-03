namespace Entertainment.Application.Repositories.Common
{
    public interface IDapperBaseWriteRepository
    {
        int EditData(string command);
        Task<int> EditDataAsync(string command);
    }
}
