using AutoMapper;
using Workout.Application.Abstractions.Services;
using Workout.Application.Abstractions.UnitOfWork;
using Workout.Application.DTOs.WorkoutDTOs;
using Workout.Domain.Entities;
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
            return _mapper.Map<List<WorkoutDto>>(result);
        }

        public async Task<List<WorkoutDto>> GetAllWorkoutsAsync()
        {
            var result = await _uow.GetReadRepository<w.Workout>().GetAllAsync();
            return _mapper.Map<List<WorkoutDto>>(result);
        }

        public List<WorkoutDto> GetUsersAllWorkouts(string userId)
        {
            var result = _uow.GetReadRepository<w.Workout>().GetUsersAll(userId);
            return _mapper.Map<List<WorkoutDto>>(result);
        }

        public async Task<List<WorkoutDto>> GetUsersAllWorkoutsAsync(string userId)
        {
            var result = await _uow.GetReadRepository<w.Workout>().GetUsersAllAsync(userId);
            return _mapper.Map<List<WorkoutDto>>(result);
        }

        public WorkoutDto GetWorkoutById(string workoutId)
        {
            var result = _uow.GetReadRepository<w.Workout>().GetById(workoutId);
            return _mapper.Map<WorkoutDto>(result);
        }

        public async Task<WorkoutDto> GetWorkoutByIdAsync(string workoutId)
        {
            var result = await _uow.GetReadRepository<w.Workout>().GetByIdAsync(workoutId);
            return _mapper.Map<WorkoutDto>(result);
        }

        public WorkoutDto UpdateWorkout(WorkoutDto model)
        {
            var result = _uow.GetWriteRepository<w.Workout>().Update(_mapper.Map<w.Workout>(model));
            return model;
        }

        public async Task<WorkoutDto> UpdateWorkoutAsync(WorkoutDto model)
        {
            var result = await _uow.GetWriteRepository<w.Workout>().UpdateAsync(_mapper.Map<w.Workout>(model));
            return model;
        }
    }
}
