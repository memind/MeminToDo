using Entertainment.Application.Repositories.Common;
using Entertainment.Application.Repositories.ShowRepositories;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Repositories.ShowRepositories
{
    public class ShowReadRepository : IShowReadRepository
    {
        private readonly IDapperBaseReadRepository _service;

        public ShowReadRepository(IDapperBaseReadRepository service)
        {
            _service = service;
        }
        public List<Show> GetAll()
        {
            var showList = _service.GetAll<Show>("SELECT * FROM \"Shows\"");
            return showList;
        }

        public async Task<List<Show>> GetAllAsync()
        {
            var showList = await _service.GetAllAsync<Show>("SELECT * FROM \"Shows\"");
            return showList;
        }

        public Show GetById(string id)
        {
            var show = _service.Get<Show>($"SELECT * FROM \"Shows\" WHERE \"Id\" = '{id}'");
            return show;
        }

        public async Task<Show> GetByIdAsync(string id)
        {
            var show = await _service.GetAsync<Show>($"SELECT * FROM \"Shows\" WHERE \"Id\" = '{id}'");
            return show;
        }

        public List<Show> GetUsersAll(string userId)
        {
            var showList = _service.GetAll<Show>($"SELECT * FROM \"Shows\" WHERE \"UserId\" = '{userId}'");
            return showList;
        }

        public async Task<List<Show>> GetUsersAllAsync(string userId)
        {
            var showList = await _service.GetAllAsync<Show>($"SELECT * FROM \"Shows\" WHERE \"UserId\" = '{userId}'");
            return showList;
        }
    }
}
