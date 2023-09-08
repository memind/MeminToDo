using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Workout.Application.Abstractions.Services;
using Workout.Application.Abstractions.UnitOfWork;
using Workout.Application.DTOs.WorkoutDTOs;
using w = Workout.Domain.Entities;

namespace Workout.Persistance.Concretes.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<WorkoutService> _logger;

        public WorkoutService(IMapper mapper, IUnitOfWork uow, ILogger<WorkoutService> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
            _logger.LogInformation("Workout Service Is On");
        }

        public WorkoutDto CreateWorkout(WorkoutDto model)
        {
            try
            {
                _uow.GetWriteRepository<w.Workout>().Create(_mapper.Map<w.Workout>(model));
                _uow.Save();

                _logger.LogInformation($"Created Workout: {model.Name}");

                return model;
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<WorkoutDto> CreateWorkoutAsync(WorkoutDto model)
        {
            try
            {
                await _uow.GetWriteRepository<w.Workout>().CreateAsync(_mapper.Map<w.Workout>(model));
                await _uow.SaveAsync();

                _logger.LogInformation($"Created Workout: {model.Name}");

                return model;
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw;}
        }

        public void DeleteWorkout(string workoutId)
        {
            try
            {
                _uow.GetWriteRepository<w.Workout>().DeleteById(workoutId);
                _uow.Save();

                _logger.LogInformation($"Deleted Workout: {workoutId}");

            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task DeleteWorkoutAsync(string workoutId)
        {
            try
            {
                await _uow.GetWriteRepository<w.Workout>().DeleteByIdAsync(workoutId);
                await _uow.SaveAsync();

                _logger.LogInformation($"Deleted Workout: {workoutId}");

            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public List<WorkoutDto> GetAllWorkouts()
        {
            try
            {
                var result = _uow.GetReadRepository<w.Workout>().GetAll();
                var exercises = _uow.GetReadRepository<w.Exercise>().GetAll();

                _logger.LogInformation("Getting All Workouts");

                return _mapper.Map<List<WorkoutDto>>(result);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<List<WorkoutDto>> GetAllWorkoutsAsync()
        {
            try
            {
                var result = await _uow.GetReadRepository<w.Workout>().GetAllAsync();
                var exercises = await _uow.GetReadRepository<w.Exercise>().GetAllAsync();

                _logger.LogInformation("Getting All Workouts");

                return _mapper.Map<List<WorkoutDto>>(result);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public List<WorkoutDto> GetUsersAllWorkouts(string userId)
        {
            try
            {
                var result = _uow.GetReadRepository<w.Workout>().GetUsersAll(userId);
                var exercises = _uow.GetReadRepository<w.Exercise>().GetAll();

                _logger.LogInformation($"Getting Users All Workouts: {userId}");

                return _mapper.Map<List<WorkoutDto>>(result);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<List<WorkoutDto>> GetUsersAllWorkoutsAsync(string userId)
        {
            try
            {
                var result = await _uow.GetReadRepository<w.Workout>().GetUsersAllAsync(userId);
                var exercises = await _uow.GetReadRepository<w.Exercise>().GetAllAsync();

                _logger.LogInformation($"Getting Users All Workouts: {userId}");

                return _mapper.Map<List<WorkoutDto>>(result);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public WorkoutDto GetWorkoutById(string workoutId)
        {
            try
            {
                var result = _uow.GetReadRepository<w.Workout>().GetById(workoutId);
                var exercises = _uow.GetReadRepository<w.Exercise>().GetAll();

                _logger.LogInformation($"Getting Workout: {workoutId}");

                return _mapper.Map<WorkoutDto>(result);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }

        public async Task<WorkoutDto> GetWorkoutByIdAsync(string workoutId)
        {
            try
            {
                var result = await _uow.GetReadRepository<w.Workout>().GetByIdAsync(workoutId);
                var exercises = (await _uow.GetReadRepository<w.Exercise>().GetAllAsync());

                _logger.LogInformation($"Getting Workout: {workoutId}");

                return _mapper.Map<WorkoutDto>(result);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
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
                _logger.LogInformation($"Updated Workout: {id}");

                return _mapper.Map<WorkoutDto>(map);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
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
                _logger.LogInformation($"Updated Workout: {id}");

                return _mapper.Map<WorkoutDto>(map);
            } catch (Exception error) { _logger.LogError($"An error occured: {error.Message}"); throw; }
        }
    }
}
