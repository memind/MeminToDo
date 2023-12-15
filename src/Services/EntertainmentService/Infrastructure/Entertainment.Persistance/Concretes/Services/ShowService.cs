using AutoMapper;
using Common.Logging.Logs.EntertainmentLogs;
using Entertainment.Application.Abstractions.Services;
using Entertainment.Application.DTOs.ShowDTOs;
using Entertainment.Application.Repositories.ShowRepositories;
using Entertainment.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Entertainment.Persistance.Concretes.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowReadRepository _read;
        private readonly IShowWriteRepository _write;
        private readonly IMapper _mapper;
        private readonly ILogger<ShowService> _logger;

        public ShowService(IShowWriteRepository bookWriteRepository, IShowReadRepository bookReadRepository, IMapper mapper, ILogger<ShowService> logger)
        {
            _write = bookWriteRepository;
            _read = bookReadRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public int CreateShow(ShowDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.CreateShow(entity.ShowName, entity.UserId));
                return _write.Create(_mapper.Map<Show>(entity));
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> CreateShowAsync(ShowDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.CreateShow(entity.ShowName, entity.UserId));
                return await _write.CreateAsync(_mapper.Map<Show>(entity));
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public int DeleteShow(string id)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.DeleteShow(id));
                return _write.Delete(id);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> DeleteShowAsync(string id)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.DeleteShow(id));
                return await _write.DeleteAsync(id);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<ShowDto> GetAllShows()
        {
            try
            {
                var games = _read.GetAll();

                _logger.LogInformation(EntertainmentLogs.GetAllShows());

                return _mapper.Map<List<ShowDto>>(games);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<ShowDto>> GetAllShowsAsync()
        {
            try
            {
                var games = await _read.GetAllAsync();

                _logger.LogInformation(EntertainmentLogs.GetAllShows());

                return _mapper.Map<List<ShowDto>>(games);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public ShowDto GetShowById(string id)
        {
            try
            {
                var game = _read.GetById(id);

                _logger.LogInformation(EntertainmentLogs.GetShowById(id));

                return _mapper.Map<ShowDto>(game);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<ShowDto> GetShowByIdAsync(string id)
        {
            try
            {
                var game = await _read.GetByIdAsync(id);

                _logger.LogInformation(EntertainmentLogs.GetShowById(id));

                return _mapper.Map<ShowDto>(game);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<ShowDto> GetUsersAllShows(string userId)
        {
            try
            {
                var games = _read.GetUsersAll(userId);

                _logger.LogInformation(EntertainmentLogs.GetUsersAllShows(userId));

                return _mapper.Map<List<ShowDto>>(games);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<ShowDto>> GetUsersAllShowsAsync(string userId)
        {
            try
            {
                var games = await _read.GetUsersAllAsync(userId);

                _logger.LogInformation(EntertainmentLogs.GetUsersAllShows(userId));

                return _mapper.Map<List<ShowDto>>(games);
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public int UpdateShow(ShowDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.UpdateShow(entity.ShowName, entity.UserId));
                return _write.Update(_mapper.Map<Show>(entity));
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<int> UpdateShowAsync(ShowDto entity)
        {
            try
            {
                _logger.LogInformation(EntertainmentLogs.UpdateShow(entity.ShowName, entity.UserId));
                return await _write.UpdateAsync(_mapper.Map<Show>(entity));
            }
            catch (Exception error) { _logger.LogError(EntertainmentLogs.AnErrorOccured(error.Message)); throw; }
        }
    }
}
