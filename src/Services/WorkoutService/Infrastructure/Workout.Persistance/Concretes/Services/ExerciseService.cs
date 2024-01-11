using AutoMapper;
using Common.Caching.Services;
using Common.Logging.Logs.WorkoutLogs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Workout.Application.Abstractions.Services;
using Workout.Application.Abstractions.UnitOfWork;
using Workout.Application.DTOs.ExerciseDTOs;
using Workout.Application.DTOs.WorkoutDTOs;
using Workout.Domain.Entities;
using w = Workout.Domain.Entities;
using Workout.Persistance.Consts;
using Thrift.Protocol.Entities;
using StackExchange.Redis;
using Common.Messaging.RabbitMQ.Abstract;

namespace Workout.Persistance.Concretes.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ExerciseService> _logger;
        private readonly IDatabase _cache;
        private readonly IMessageConsumerService _message;

        public ExerciseService(IMapper mapper, IUnitOfWork uow, ILogger<ExerciseService> logger, IMessageConsumerService message)
        {
            _cache = RedisService.GetRedisMasterDatabase();
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
            _message = message;

            _message.PublishConnectedInfo(MessageConsts.ExerciseServiceName());
        }

        public ExerciseDto CreateExercise(ExerciseDto model)
        {
            try
            {
                _uow.GetWriteRepository<w.Exercise>().Create(_mapper.Map<w.Exercise>(model));
                _uow.Save();

                _logger.LogInformation(WorkoutLogs.CreateExercise(model.Name));

                return model;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<ExerciseDto> CreateExerciseAsync(ExerciseDto model)
        {
            try
            {
                await _uow.GetWriteRepository<w.Exercise>().CreateAsync(_mapper.Map<w.Exercise>(model));
                await _uow.SaveAsync();

                _logger.LogInformation(WorkoutLogs.CreateExercise(model.Name));

                return model;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public void DeleteExercise(string exerciseId)
        {
            try
            {
                _uow.GetWriteRepository<w.Exercise>().DeleteById(exerciseId);
                _uow.Save();

                _logger.LogInformation(WorkoutLogs.DeleteExercise(exerciseId));
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task DeleteExerciseAsync(string exerciseId)
        {
            try
            {
                await _uow.GetWriteRepository<w.Exercise>().DeleteByIdAsync(exerciseId);
                await _uow.SaveAsync();

                _logger.LogInformation(WorkoutLogs.DeleteExercise(exerciseId));
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<ExerciseDto> GetAllExercises()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllExercises();
                var cachedExercises = _cache.StringGet(cacheKey);

                if (!cachedExercises.IsNull)
                    return JsonConvert.DeserializeObject<List<ExerciseDto>>(cachedExercises);

                var result = _uow.GetReadRepository<w.Exercise>().GetAll();
                var map = _mapper.Map<List<ExerciseDto>>(result);

                var serializedWorkouts = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedWorkouts);

                _logger.LogInformation(WorkoutLogs.GetAllExercises());

                return map;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<ExerciseDto>> GetAllExercisesAsync()
        {
            try
            {
                var cacheKey = CacheConsts.GetAllExercises();
                var cachedExercises = await _cache.StringGetAsync(cacheKey);

                if (!cachedExercises.IsNull)
                    return JsonConvert.DeserializeObject<List<ExerciseDto>>(cachedExercises);

                var result = await _uow.GetReadRepository<w.Exercise>().GetAllAsync();
                var map = _mapper.Map<List<ExerciseDto>>(result);

                var serializedWorkouts = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedWorkouts);

                _logger.LogInformation(WorkoutLogs.GetAllExercises());

                return map;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<ExerciseDto> GetAllExercisesInWorkout(string workoutId)
        {
            try
            {
                var cacheKey = CacheConsts.GetAllExercisesInWorkout(workoutId);
                var cachedExercises = _cache.StringGet(cacheKey);

                if (!cachedExercises.IsNull)
                    return JsonConvert.DeserializeObject<List<ExerciseDto>>(cachedExercises);

                var result = _uow.GetReadRepository<w.Exercise>().GetAll().Where(e => e.WorkoutId == workoutId).ToList();
                var map = _mapper.Map<List<ExerciseDto>>(result);

                var serializedExercises = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedExercises);

                _logger.LogInformation(WorkoutLogs.GetAllExercisesInWorkout(workoutId));

                return map;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<ExerciseDto>> GetAllExercisesInWorkoutAsync(string workoutId)
        {
            try
            {
                var cacheKey = CacheConsts.GetAllExercisesInWorkout(workoutId);
                var cachedExercises = await _cache.StringGetAsync(cacheKey);

                if (!cachedExercises.IsNull)
                    return JsonConvert.DeserializeObject<List<ExerciseDto>>(cachedExercises);

                var result = (await _uow.GetReadRepository<w.Exercise>().GetAllAsync()).Where(e => e.WorkoutId == workoutId).ToList();
                var map = _mapper.Map<List<ExerciseDto>>(result);

                var serializedExercises = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedExercises);

                _logger.LogInformation(WorkoutLogs.GetAllExercisesInWorkout(workoutId));

                return map;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public ExerciseDto GetExerciseById(string exerciseId)
        {
            try
            {
                var cacheKey = CacheConsts.GetExerciseById(exerciseId);
                var cachedExercises = _cache.StringGet(cacheKey);

                if (!cachedExercises.IsNull)
                    return JsonConvert.DeserializeObject<ExerciseDto>(cachedExercises);

                var result = _uow.GetReadRepository<w.Exercise>().GetById(exerciseId);
                result.Workout = _uow.GetReadRepository<w.Workout>().GetById(result.WorkoutId);
                var map = _mapper.Map<ExerciseDto>(result);

                var serializedExercises = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedExercises);

                _logger.LogInformation(WorkoutLogs.GetExerciseById(exerciseId));

                return map;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<ExerciseDto> GetExerciseByIdAsync(string exerciseId)
        {
            try
            {
                var cacheKey = CacheConsts.GetExerciseById(exerciseId);
                var cachedExercises = await _cache.StringGetAsync(cacheKey);

                if (!cachedExercises.IsNull)
                    return JsonConvert.DeserializeObject<ExerciseDto>(cachedExercises);

                var result = await _uow.GetReadRepository<w.Exercise>().GetByIdAsync(exerciseId);
                result.Workout = await _uow.GetReadRepository<w.Workout>().GetByIdAsync(result.WorkoutId);
                var map = _mapper.Map<ExerciseDto>(result);


                var serializedExercises = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedExercises);

                _logger.LogInformation(WorkoutLogs.GetExerciseById(exerciseId));

                return map;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<ExerciseDto> GetUsersAllExercises(string userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllExercises(userId);
                var cachedExercises = _cache.StringGet(cacheKey);

                if (!cachedExercises.IsNull)
                    return JsonConvert.DeserializeObject<List<ExerciseDto>>(cachedExercises);

                var result = _uow.GetReadRepository<w.Exercise>().GetUsersAll(userId);
                var map = _mapper.Map<List<ExerciseDto>>(result);

                var serializedExercises = JsonConvert.SerializeObject(map);
                _cache.StringSet(cacheKey, serializedExercises);

                _logger.LogInformation(WorkoutLogs.GetUsersAllExercises(userId));

                return map;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<ExerciseDto>> GetUsersAllExercisesAsync(string userId)
        {
            try
            {
                var cacheKey = CacheConsts.GetUsersAllExercises(userId);
                var cachedExercises = await _cache.StringGetAsync(cacheKey);

                if (!cachedExercises.IsNull)
                    return JsonConvert.DeserializeObject<List<ExerciseDto>>(cachedExercises);

                var result = await _uow.GetReadRepository<w.Exercise>().GetUsersAllAsync(userId);
                var map = _mapper.Map<List<ExerciseDto>>(result);

                var serializedExercises = JsonConvert.SerializeObject(map);
                await _cache.StringSetAsync(cacheKey, serializedExercises);

                _logger.LogInformation(WorkoutLogs.GetUsersAllExercises(userId));

                return map;
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public ExerciseDto UpdateExercise(ExerciseDto model, string id)
        {
            try
            {
                var result = _uow.GetReadRepository<w.Exercise>().GetById(id);

                var map = _mapper.Map(model, result);
                result.UpdatedDate = DateTime.UtcNow;

                _uow.Save();
                _logger.LogInformation(WorkoutLogs.UpdateExercise(id));

                return _mapper.Map<ExerciseDto>(map);
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<ExerciseDto> UpdateExerciseAsync(ExerciseDto model, string id)
        {
            try
            {
                var result = await _uow.GetReadRepository<w.Exercise>().GetByIdAsync(id);

                var map = _mapper.Map(model, result);
                result.UpdatedDate = DateTime.UtcNow;

                await _uow.SaveAsync();
                _logger.LogInformation(WorkoutLogs.UpdateExercise(id));

                return _mapper.Map<ExerciseDto>(map);
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public void ConsumeBackUpInfo() => _message.ConsumeBackUpInfo();

        public void ConsumeTestInfo() => _message.ConsumeStartTest();
    }
}
