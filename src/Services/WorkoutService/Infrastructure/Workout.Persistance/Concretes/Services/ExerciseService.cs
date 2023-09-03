using AutoMapper;
using Workout.Application.Abstractions.Services;
using Workout.Application.Abstractions.UnitOfWork;
using Workout.Application.DTOs.ExerciseDTOs;
using Workout.Application.Repositories.ExerciseRepositories;
using Workout.Domain.Entities;

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
            _uow.GetWriteRepository<Exercise>().Create(_mapper.Map<Exercise>(model));
            _uow.Save();
            return model;
        }

        public async Task<ExerciseDto> CreateExerciseAsync(ExerciseDto model)
        {
            await _uow.GetWriteRepository<Exercise>().CreateAsync(_mapper.Map<Exercise>(model));
            await _uow.SaveAsync();
            return model;
        }

        public void DeleteExercise(string exerciseId)
        {
            _uow.GetWriteRepository<Exercise>().DeleteById(exerciseId);
            _uow.Save();
        }

        public async Task DeleteExerciseAsync(string exerciseId)
        {
            await _uow.GetWriteRepository<Exercise>().DeleteByIdAsync(exerciseId);
            await _uow.SaveAsync();
        }

        public List<ExerciseDto> GetAllExercises()
        {
            var result = _uow.GetReadRepository<Exercise>().GetAll();
            return _mapper.Map<List<ExerciseDto>>(result);
        }

        public async Task<List<ExerciseDto>> GetAllExercisesAsync()
        {
            var result = await _uow.GetReadRepository<Exercise>().GetAllAsync();
            return _mapper.Map<List<ExerciseDto>>(result);
        }

        public List<ExerciseDto> GetAllExercisesInWorkout(string workoutId)
        {
            var result = _uow.GetReadRepository<Exercise>().GetAll().Where(e => e.WorkoutId == workoutId).ToList();
            return _mapper.Map<List<ExerciseDto>>(result);
        }

        public async Task<List<ExerciseDto>> GetAllExercisesInWorkoutAsync(string workoutId)
        {
            var result = (await _uow.GetReadRepository<Exercise>().GetAllAsync()).Where(e => e.WorkoutId == workoutId).ToList();
            return _mapper.Map<List<ExerciseDto>>(result);
        }

        public ExerciseDto GetExerciseById(string exerciseId)
        {
            var result = _uow.GetReadRepository<Exercise>().GetById(exerciseId);
            return _mapper.Map<ExerciseDto>(result);
        }

        public async Task<ExerciseDto> GetExerciseByIdAsync(string exerciseId)
        {
            var result = await _uow.GetReadRepository<Exercise>().GetByIdAsync(exerciseId);
            return _mapper.Map<ExerciseDto>(result);
        }

        public List<ExerciseDto> GetUsersAllExercises(string userId)
        {
            var result = _uow.GetReadRepository<Exercise>().GetUsersAll(userId);
            return _mapper.Map<List<ExerciseDto>>(result);
        }

        public async Task<List<ExerciseDto>> GetUsersAllExercisesAsync(string userId)
        {
            var result = await _uow.GetReadRepository<Exercise>().GetUsersAllAsync(userId);
            return _mapper.Map<List<ExerciseDto>>(result);
        }

        public ExerciseDto UpdateExercise(ExerciseDto model)
        {
            var result = _uow.GetWriteRepository<Exercise>().Update(_mapper.Map<Exercise>(model));
            return model;
        }

        public async Task<ExerciseDto> UpdateExerciseAsync(ExerciseDto model)
        {
            var result = await _uow.GetWriteRepository<Exercise>().UpdateAsync(_mapper.Map<Exercise>(model));
            return model;
        }
    }
}
