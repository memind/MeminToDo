using Entertainment.Application.DTOs.ShowDTOs;
using Entertainment.Domain.Entities;

namespace Entertainment.Application.Abstractions.Services
{
    public interface IShowService
    {
        int CreateShow(ShowDto entity);
        Task<int> CreateShowAsync(ShowDto entity);

        int DeleteShow(string id);
        Task<int> DeleteShowAsync(string id);

        int UpdateShow(ShowDto entity);
        Task<int> UpdateShowAsync(ShowDto entity);

        public List<ShowDto> GetAllShows();
        Task<List<ShowDto>> GetAllShowsAsync();

        ShowDto GetShowById(string id);
        Task<ShowDto> GetShowByIdAsync(string id);

        List<ShowDto> GetUsersAllShows(string userId);
        Task<List<ShowDto>> GetUsersAllShowsAsync(string userId);
    }
}
