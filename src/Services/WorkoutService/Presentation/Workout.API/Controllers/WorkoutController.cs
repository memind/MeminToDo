using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Workout.Application.Abstractions.Services;
using Workout.Application.DTOs.WorkoutDTOs;

namespace Workout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _service;

        public WorkoutController(IWorkoutService service) => _service = service;
        

        [HttpPost]
        [Authorize(Policy = "WorkoutWrite")]
        public WorkoutDto CreateWorkout(WorkoutDto model) => _service.CreateWorkout(model);

        [HttpGet("/{id}")]
        [Authorize(Policy = "WorkoutRead")]
        public WorkoutDto GetWorkout(string id) => _service.GetWorkoutById(id);

        [HttpGet]
        [Authorize(Policy = "WorkoutRead")]
        public List<WorkoutDto> GetAllWorkouts() => _service.GetAllWorkouts();

        [HttpGet("/user/{id}")]
        [Authorize(Policy = "WorkoutRead")]
        public List<WorkoutDto> GetUsersAllWorkouts(string id) => _service.GetUsersAllWorkouts(id);

        [HttpPut]
        [Authorize(Policy = "WorkoutWrite")]
        public WorkoutDto UpdateWorkout(WorkoutDto model, string id) => _service.UpdateWorkout(model, id);

        [HttpDelete]
        [Authorize(Policy = "WorkoutWrite")]
        public void DeleteWorkout(string id) => _service.DeleteWorkout(id);

        [HttpGet("/consumeBackup")]
        public void ConsumeBackUpInfo() => _service.ConsumeBackUpInfo();

        [HttpGet("/consumeTest")]
        public void ConsumeTestInfo() => _service.ConsumeTestInfo();
    }
}
