using AutoMapper;
using Workout.Application.Abstractions.Services;
using Workout.Application.Abstractions.UnitOfWork;
using Workout.Application.DTOs.ExerciseDTOs;
using Workout.Application.DTOs.WorkoutDTOs;
using w = Workout.Domain.Entities;

namespace Workout.Persistance.Concretes.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ExerciseService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public ExerciseDto CreateExercise(ExerciseDto model)
        {
            _uow.GetWriteRepository<w.Exercise>().Create(_mapper.Map<w.Exercise>(model));
            _uow.Save();
            return model;
        }

        public async Task<ExerciseDto> CreateExerciseAsync(ExerciseDto model)
        {
            await _uow.GetWriteRepository<w.Exercise>().CreateAsync(_mapper.Map<w.Exercise>(model));
            await _uow.SaveAsync();
            return model;
        }

        public void DeleteExercise(string exerciseId)
        {
            _uow.GetWriteRepository<w.Exercise>().DeleteById(exerciseId);
            _uow.Save();
        }

        public async Task DeleteExerciseAsync(string exerciseId)
        {
            await _uow.GetWriteRepository<w.Exercise>().DeleteByIdAsync(exerciseId);
            await _uow.SaveAsync();
        }

        public List<ExerciseDto> GetAllExercises()
        {
            var result = _uow.GetReadRepository<w.Exercise>().GetAll();
            return _mapper.Map<List<ExerciseDto>>(result);
        }

        public async Task<List<ExerciseDto>> GetAllExercisesAsync()
        {
            var result = await _uow.GetReadRepository<w.Exercise>().GetAllAsync();
            return _mapper.Map<List<ExerciseDto>>(result);
        }

        public List<ExerciseDto> GetAllExercisesInWorkout(string workoutId)
        {
            var result = _uow.GetReadRepository<w.Exercise>().GetAll().Where(e => e.WorkoutId == workoutId).ToList();
            return _mapper.Map<List<ExerciseDto>>(result);
        }

        public async Task<List<ExerciseDto>> GetAllExercisesInWorkoutAsync(string workoutId)
        {
            var result = (await _uow.GetReadRepository<w.Exercise>().GetAllAsync()).Where(e => e.WorkoutId == workoutId).ToList();
            return _mapper.Map<List<ExerciseDto>>(result);
        }

        public ExerciseDto GetExerciseById(string exerciseId)
        {
            var result = _uow.GetReadRepository<w.Exercise>().GetById(exerciseId);
            result.Workout = _uow.GetReadRepository<w.Workout>().GetById(result.WorkoutId);
            return _mapper.Map<ExerciseDto>(result);
        }

        public async Task<ExerciseDto> GetExerciseByIdAsync(string exerciseId)
        {
            var result = await _uow.GetReadRepository<w.Exercise>().GetByIdAsync(exerciseId);
            return _mapper.Map<ExerciseDto>(result);
        }

        public List<ExerciseDto> GetUsersAllExercises(string userId)
        {
            var result = _uow.GetReadRepository<w.Exercise>().GetUsersAll(userId);
            return _mapper.Map<List<ExerciseDto>>(result);
        }

        public async Task<List<ExerciseDto>> GetUsersAllExercisesAsync(string userId)
        {
            var result = await _uow.GetReadRepository<w.Exercise>().GetUsersAllAsync(userId);
            return _mapper.Map<List<ExerciseDto>>(result);
        }

        public ExerciseDto UpdateExercise(ExerciseDto model, string id)
        {
            var result = _uow.GetReadRepository<w.Exercise>().GetById(id);

            var map = _mapper.Map(model, result);
            result.UpdatedDate = DateTime.UtcNow;

            _uow.Save();
            return _mapper.Map<ExerciseDto>(map);
        }

        public async Task<ExerciseDto> UpdateExerciseAsync(ExerciseDto model, string id)
        {
            var result = await _uow.GetReadRepository<w.Exercise>().GetByIdAsync(id);

            var map = _mapper.Map(model, result);
            result.UpdatedDate = DateTime.UtcNow;

            await _uow.SaveAsync();
            return _mapper.Map<ExerciseDto>(map);
        }
    }
}
