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

        public WorkoutController(IWorkoutService service)
        {
            _service = service;
        }

        [HttpPost("/createWorkout")]
        [Authorize(Policy = "WorkoutWrite")]
        public WorkoutDto CreateWorkout(WorkoutDto model)
        {
            return _service.CreateWorkout(model);
        }

        [HttpGet("/getWorkout")]
        [Authorize(Policy = "WorkoutRead")]
        public WorkoutDto GetWorkout(string id)
        {
            return _service.GetWorkoutById(id);
        }

        [HttpGet("/getAllWorkouts")]
        [Authorize(Policy = "WorkoutRead")]
        public List<WorkoutDto> GetAllWorkouts()
        {
            return _service.GetAllWorkouts();
        }

        [HttpGet("/getUsersAllWorkouts")]
        [Authorize(Policy = "WorkoutRead")]
        public List<WorkoutDto> GetUsersAllWorkouts(string id)
        {
            return _service.GetUsersAllWorkouts(id);
        }

        [HttpPut("/updateWorkout")]
        [Authorize(Policy = "WorkoutWrite")]
        public WorkoutDto UpdateWorkout(WorkoutDto model, string id)
        {
            return _service.UpdateWorkout(model, id);
        }

        [HttpDelete("/deleteWorkout")]
        [Authorize(Policy = "WorkoutWrite")]
        public void DeleteWorkout(string id)
        {
            _service.DeleteWorkout(id);
        }
    }
}
