using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Common.Caching.Services;
using Common.Logging.Logs.WorkoutLogs;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using Workout.Application.Abstractions.Services;
using Workout.Application.Abstractions.UnitOfWork;
using Workout.Application.DTOs.WorkoutDTOs;
using w = Workout.Domain.Entities;
using Workout.Persistance.Consts;
using Common.Messaging.RabbitMQ.Abstract;

namespace Workout.Persistance.Concretes.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<WorkoutService> _logger;
        private readonly IDatabase _cache;
        private readonly IMessageConsumerService _message;

        public WorkoutService(IMapper mapper, IUnitOfWork uow, ILogger<WorkoutService> logger, IMessageConsumerService message)
        {
            _cache = RedisService.GetRedisMasterDatabase();
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
            _message = message;

            _message.PublishConnectedInfo(MessageConsts.WorkoutServiceName());
        }

        public WorkoutDto CreateWorkout(WorkoutDto model)
        {
            try
            {
                _uow.GetWriteRepository<w.Workout>().Create(_mapper.Map<w.Workout>(model));
                _uow.Save();

                _logger.LogInformation(WorkoutLogs.CreateWorkout(model.Name));

                return model;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<WorkoutDto> CreateWorkoutAsync(WorkoutDto model)
        {
            try
            {
                await _uow.GetWriteRepository<w.Workout>().CreateAsync(_mapper.Map<w.Workout>(model));
                await _uow.SaveAsync();

                _logger.LogInformation(WorkoutLogs.CreateWorkout(model.Name));

                return model;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw;}
        }

        public void DeleteWorkout(string workoutId)
        {
            try
            {
                _uow.GetWriteRepository<w.Workout>().DeleteById(workoutId);
                _uow.Save();

                _logger.LogInformation(WorkoutLogs.DeleteWorkout(workoutId));

            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task DeleteWorkoutAsync(string workoutId)
        {
            try
            {
                await _uow.GetWriteRepository<w.Workout>().DeleteByIdAsync(workoutId);
                await _uow.SaveAsync();

                _logger.LogInformation(WorkoutLogs.DeleteWorkout(workoutId));

            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<WorkoutDto> GetAllWorkouts()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllWorkouts();
                var cachedWorkouts = _cache.StringGet(cacheKey);

                if (!cachedWorkouts.IsNull)
                    return JsonConvert.DeserializeObject<List<WorkoutDto>>(cachedWorkouts);

                var result = _uow.GetReadRepository<w.Workout>().GetAll();
                var exercises = _uow.GetReadRepository<w.Exercise>().GetAll();
                var map = _mapper.Map<List<WorkoutDto>>(result);

                var serializedWorkouts = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedWorkouts);

                _logger.LogInformation(WorkoutLogs.GetAllWorkouts());

                return map;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<WorkoutDto>> GetAllWorkoutsAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllWorkouts();
                var cachedWorkouts = await _cache.StringGetAsync(cacheKey);

                if (!cachedWorkouts.IsNull)
                    return JsonConvert.DeserializeObject<List<WorkoutDto>>(cachedWorkouts);

                var result = await _uow.GetReadRepository<w.Workout>().GetAllAsync();
                var exercises =await  _uow.GetReadRepository<w.Exercise>().GetAllAsync();
                var map = _mapper.Map<List<WorkoutDto>>(result);

                var serializedWorkouts = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedWorkouts);

                _logger.LogInformation(WorkoutLogs.GetAllWorkouts());

                return map;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<WorkoutDto> GetUsersAllWorkouts(string userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllWorkouts(userId);
                var cachedWorkouts = _cache.StringGet(cacheKey);

                if (!cachedWorkouts.IsNull)
                    return JsonConvert.DeserializeObject<List<WorkoutDto>>(cachedWorkouts);

                var result = _uow.GetReadRepository<w.Workout>().GetUsersAll(userId);
                var exercises = _uow.GetReadRepository<w.Exercise>().GetAll();
                var map = _mapper.Map<List<WorkoutDto>>(result);

                var serializedWorkouts = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedWorkouts);

                _logger.LogInformation(WorkoutLogs.GetUsersAllWorkouts(userId));

                return map;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<WorkoutDto>> GetUsersAllWorkoutsAsync(string userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllWorkouts(userId);
                var cachedWorkouts = await _cache.StringGetAsync(cacheKey);

                if (!cachedWorkouts.IsNull)
                    return JsonConvert.DeserializeObject<List<WorkoutDto>>(cachedWorkouts);

                var result = await _uow.GetReadRepository<w.Workout>().GetUsersAllAsync(userId);
                var exercises = await _uow.GetReadRepository<w.Exercise>().GetAllAsync();
                var map = _mapper.Map<List<WorkoutDto>>(result);

                var serializedWorkouts = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedWorkouts);

                _logger.LogInformation(WorkoutLogs.GetUsersAllWorkouts(userId));

                return map;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public WorkoutDto GetWorkoutById(string workoutId)
        {
            try
            {
                var cacheKey = CacheConsts.GetWorkoutById(workoutId);
                var cachedWorkout = _cache.StringGet(cacheKey);

                if (!cachedWorkout.IsNull)
                    return JsonConvert.DeserializeObject<WorkoutDto>(cachedWorkout);

                var result = _uow.GetReadRepository<w.Workout>().GetById(workoutId);
                var exercises = _uow.GetReadRepository<w.Exercise>().GetAll();
                var map = _mapper.Map<WorkoutDto>(result);

                var serializedWorkout = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedWorkout);

                _logger.LogInformation(WorkoutLogs.GetWorkoutById(workoutId));

                return map;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<WorkoutDto> GetWorkoutByIdAsync(string workoutId)
        {
            try
            {
                var cacheKey = CacheConsts.GetWorkoutById(workoutId);
                var cachedWorkout = await _cache.StringGetAsync(cacheKey);

                if (!cachedWorkout.IsNull)
                    return JsonConvert.DeserializeObject<WorkoutDto>(cachedWorkout);

                var result = await _uow.GetReadRepository<w.Workout>().GetByIdAsync(workoutId);
                var exercises = await _uow.GetReadRepository<w.Exercise>().GetAllAsync();
                var map = _mapper.Map<WorkoutDto>(result);

                var serializedWorkout = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedWorkout);

                _logger.LogInformation(WorkoutLogs.GetWorkoutById(workoutId));

                return map;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public WorkoutDto UpdateWorkout(WorkoutDto model, string id)
        {
            try
            {
                var result = _uow.GetReadRepository<w.Workout>().GetById(id);
                var exercises = _uow.GetReadRepository<w.Exercise>().GetAll();

                result.Exercises = _mapper.Map<List<w.Exercise>>(model.Exercises);

                var map = _mapper.Map(model, result);
                result.UpdatedDate = DateTime.UtcNow;

                _uow.Save();
                _logger.LogInformation(WorkoutLogs.UpdateWorkout(id));

                return _mapper.Map<WorkoutDto>(map);
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<WorkoutDto> UpdateWorkoutAsync(WorkoutDto model, string id)
        {
            try
            {
                var result = await _uow.GetReadRepository<w.Workout>().GetByIdAsync(id);
                var exercises = await _uow.GetReadRepository<w.Exercise>().GetAllAsync();

                result.Exercises = _mapper.Map<List<w.Exercise>>(model.Exercises);

                var map = _mapper.Map(model, result);
                result.UpdatedDate = DateTime.UtcNow;

                await _uow.SaveAsync();
                _logger.LogInformation(WorkoutLogs.UpdateWorkout(id));

                return _mapper.Map<WorkoutDto>(map);
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)  ); throw; }
        }

        public void ConsumeBackUpInfo() => _message.ConsumeBackUpInfo();

        public void ConsumeTestInfo() => _message.ConsumeStartTest();
    }
}
