using AutoMapper;
using Common.Logging.Logs.WorkoutLogs;
using Microsoft.Extensions.Logging;
using Workout.Application.Abstractions.Services;
using Workout.Application.Abstractions.UnitOfWork;
using Workout.Application.DTOs.ExerciseDTOs;
using Workout.Application.DTOs.WorkoutDTOs;
using Workout.Domain.Entities;
using w = Workout.Domain.Entities;

namespace Workout.Persistance.Concretes.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ExerciseService> _logger;

        public ExerciseService(IMapper mapper, IUnitOfWork uow, ILogger<ExerciseService> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
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
                var result = _uow.GetReadRepository<w.Exercise>().GetAll();

                _logger.LogInformation(WorkoutLogs.GetAllExercises());

                return _mapper.Map<List<ExerciseDto>>(result);
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<ExerciseDto>> GetAllExercisesAsync()
        {
            try
            {
                var result = await _uow.GetReadRepository<w.Exercise>().GetAllAsync();

                _logger.LogInformation(WorkoutLogs.GetAllExercises());

                return _mapper.Map<List<ExerciseDto>>(result);
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<ExerciseDto> GetAllExercisesInWorkout(string workoutId)
        {
            try
            {
                var result = _uow.GetReadRepository<w.Exercise>().GetAll().Where(e => e.WorkoutId == workoutId).ToList();

                _logger.LogInformation(WorkoutLogs.GetAllExercisesInWorkout(workoutId));

                return _mapper.Map<List<ExerciseDto>>(result);
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<ExerciseDto>> GetAllExercisesInWorkoutAsync(string workoutId)
        {
            try
            {
                var result = (await _uow.GetReadRepository<w.Exercise>().GetAllAsync()).Where(e => e.WorkoutId == workoutId).ToList();

                _logger.LogInformation(WorkoutLogs.GetAllExercisesInWorkout(workoutId));

                return _mapper.Map<List<ExerciseDto>>(result);
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public ExerciseDto GetExerciseById(string exerciseId)
        {
            try
            {
                var result = _uow.GetReadRepository<w.Exercise>().GetById(exerciseId);
                result.Workout = _uow.GetReadRepository<w.Workout>().GetById(result.WorkoutId);

                _logger.LogInformation(WorkoutLogs.GetExerciseById(exerciseId));

                return _mapper.Map<ExerciseDto>(result);
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<ExerciseDto> GetExerciseByIdAsync(string exerciseId)
        {
            try
            {

                var result = await _uow.GetReadRepository<w.Exercise>().GetByIdAsync(exerciseId);
                result.Workout = await _uow.GetReadRepository<w.Workout>().GetByIdAsync(result.WorkoutId);

                _logger.LogInformation(WorkoutLogs.GetExerciseById(exerciseId));

                return _mapper.Map<ExerciseDto>(result);
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public List<ExerciseDto> GetUsersAllExercises(string userId)
        {
            try
            {
                var result = _uow.GetReadRepository<w.Exercise>().GetUsersAll(userId);

                _logger.LogInformation(WorkoutLogs.GetUsersAllExercises(userId));

                return _mapper.Map<List<ExerciseDto>>(result);
            } catch (Exception error) { _logger.LogError(WorkoutLogs.AnErrorOccured(error.Message)); throw; }
        }

        public async Task<List<ExerciseDto>> GetUsersAllExercisesAsync(string userId)
        {
            try
            {
                var result = await _uow.GetReadRepository<w.Exercise>().GetUsersAllAsync(userId);

                _logger.LogInformation(WorkoutLogs.GetUsersAllExercises(userId));

                return _mapper.Map<List<ExerciseDto>>(result);
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
    }
}
