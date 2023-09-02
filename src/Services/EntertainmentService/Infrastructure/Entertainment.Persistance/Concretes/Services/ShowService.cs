using AutoMapper;
using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.ShowDTOs;
using Entertainment.Application.Repositories.ShowRepositories;
using Entertainment.Domain.Entities;

namespace Entertainment.Persistance.Concretes.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowReadRepository _read;
        private readonly IShowWriteRepository _write;
        private readonly IMapper _mapper;

        public ShowService(IShowWriteRepository bookWriteRepository, IShowReadRepository bookReadRepository, IMapper mapper)
        {
            _write = bookWriteRepository;
            _read = bookReadRepository;
            _mapper = mapper;
        }

        public int CreateShow(ShowDto entity)
        {
            return _write.Create(_mapper.Map<Show>(entity));
        }

        public async Task<int> CreateShowAsync(ShowDto entity)
        {
            return await _write.CreateAsync(_mapper.Map<Show>(entity));
        }

        public int DeleteShow(string id)
        {
            return _write.Delete(id);
        }

        public async Task<int> DeleteShowAsync(string id)
        {
            return await _write.DeleteAsync(id);
        }

        public List<ShowDto> GetAllShows()
        {
            var shows = _read.GetAll();
            return _mapper.Map<List<ShowDto>>(shows);
        }

        public async Task<List<ShowDto>> GetAllShowsAsync()
        {
            var shows = await _read.GetAllAsync();
            return _mapper.Map<List<ShowDto>>(shows);
        }

        public ShowDto GetShowById(string id)
        {
            var show = _read.GetById(id);
            return _mapper.Map<ShowDto>(show);
        }

        public async Task<ShowDto> GetShowByIdAsync(string id)
        {
            var show = await _read.GetByIdAsync(id);
            return _mapper.Map<ShowDto>(show);
        }

        public List<ShowDto> GetUsersAllShows(string userId)
        {
            var shows = _read.GetUsersAll(userId);
            return _mapper.Map<List<ShowDto>>(shows);
        }

        public async Task<List<ShowDto>> GetUsersAllShowsAsync(string userId)
        {
            var shows = await _read.GetUsersAllAsync(userId);
            return _mapper.Map<List<ShowDto>>(shows);
        }

        public int UpdateShow(ShowDto entity)
        {
            return _write.Update(_mapper.Map<Show>(entity));
        }

        public async Task<int> UpdateShowAsync(ShowDto entity)
        {
            return await _write.UpdateAsync(_mapper.Map<Show>(entity));
        }
    }
}
