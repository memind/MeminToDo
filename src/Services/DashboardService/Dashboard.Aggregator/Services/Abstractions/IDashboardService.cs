using Dashboard.Aggregator.Models;

namespace Dashboard.Aggregator.Services.Abstractions
{
    public interface IDashboardService
    {
        Task<AdminDashboardModel> GetAdminDashboardInfos();
        Task<UserDashboardModel> GetUserDashboardInfos(string id);
    }
}
