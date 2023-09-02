namespace Entertainment.Application.Repositories.Common
{
    public interface IDapperBaseWriteService
    {
        int EditData(string command, object parms);
        Task<int> EditDataAsync(string command, object parms);
    }
}
