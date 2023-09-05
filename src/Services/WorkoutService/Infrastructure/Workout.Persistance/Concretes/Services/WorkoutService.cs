using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.Azure.Cosmos;
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

        public WorkoutService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public WorkoutDto CreateWorkout(WorkoutDto model)
        {
            _uow.GetWriteRepository<w.Workout>().Create(_mapper.Map<w.Workout>(model));
            _uow.Save();
            return model;
        }

        public async Task<WorkoutDto> CreateWorkoutAsync(WorkoutDto model)
        {
            await _uow.GetWriteRepository<w.Workout>().CreateAsync(_mapper.Map<w.Workout>(model));
            await _uow.SaveAsync();
            return model;
        }

        public void DeleteWorkout(string workoutId)
        {
            _uow.GetWriteRepository<w.Workout>().DeleteById(workoutId);
            _uow.Save();
        }

        public async Task DeleteWorkoutAsync(string workoutId)
        {
            await _uow.GetWriteRepository<w.Workout>().DeleteByIdAsync(workoutId);
            await _uow.SaveAsync();
        }

        public List<WorkoutDto> GetAllWorkouts()
        {
            var result = _uow.GetReadRepository<w.Workout>().GetAll();
            var exercises = _uow.GetReadRepository<w.Exercise>().GetAll();

            return _mapper.Map<List<WorkoutDto>>(result);
        }

        public async Task<List<WorkoutDto>> GetAllWorkoutsAsync()
        {
            var result = await _uow.GetReadRepository<w.Workout>().GetAllAsync();
            var exercises = await _uow.GetReadRepository<w.Exercise>().GetAllAsync();

            return _mapper.Map<List<WorkoutDto>>(result);
        }

        public List<WorkoutDto> GetUsersAllWorkouts(string userId)
        {
            var result = _uow.GetReadRepository<w.Workout>().GetUsersAll(userId);
            var exercises = _uow.GetReadRepository<w.Exercise>().GetAll();

            return _mapper.Map<List<WorkoutDto>>(result);
        }

        public async Task<List<WorkoutDto>> GetUsersAllWorkoutsAsync(string userId)
        {
            var result = await _uow.GetReadRepository<w.Workout>().GetUsersAllAsync(userId);
            var exercises = await _uow.GetReadRepository<w.Exercise>().GetAllAsync();

            return _mapper.Map<List<WorkoutDto>>(result);
        }

        public WorkoutDto GetWorkoutById(string workoutId)
        {
            var result = _uow.GetReadRepository<w.Workout>().GetById(workoutId);
            var exercises = _uow.GetReadRepository<w.Exercise>().GetAll();

            return _mapper.Map<WorkoutDto>(result);
        }

        public async Task<WorkoutDto> GetWorkoutByIdAsync(string workoutId)
        {
            var result = await _uow.GetReadRepository<w.Workout>().GetByIdAsync(workoutId);
            var exercises = (await _uow.GetReadRepository<w.Exercise>().GetAllAsync());

            return _mapper.Map<WorkoutDto>(result);
        }

        public WorkoutDto UpdateWorkout(WorkoutDto model, string id)
        {
            var result = _uow.GetReadRepository<w.Workout>().GetById(id);
            var exercises =  _uow.GetReadRepository<w.Exercise>().GetAll();

            result.Exercises = _mapper.Map<List<w.Exercise>>(model.Exercises);

            var map = _mapper.Map(model, result);
            result.UpdatedDate = DateTime.UtcNow;

            _uow.Save();
            return _mapper.Map<WorkoutDto>(map);
        }

        public async Task<WorkoutDto> UpdateWorkoutAsync(WorkoutDto model, string id)
        {
            var result = await _uow.GetReadRepository<w.Workout>().GetByIdAsync(id);
            var exercises = await _uow.GetReadRepository<w.Exercise>().GetAllAsync();

            result.Exercises = _mapper.Map<List<w.Exercise>>(model.Exercises);

            var map = _mapper.Map(model, result);
            result.UpdatedDate = DateTime.UtcNow;

            await _uow.SaveAsync();
            return _mapper.Map<WorkoutDto>(map);
        }
    }
}
