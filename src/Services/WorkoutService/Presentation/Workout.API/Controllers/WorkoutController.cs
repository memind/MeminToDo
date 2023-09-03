using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workout.Application.Abstractions.Services;
using Workout.Application.DTOs.WorkoutDTOs;

namespace Workout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _service;

        public WorkoutController(IWorkoutService service)
        {
            _service = service;
        }

        [HttpPost("/createWorkout")]
        public WorkoutDto CreateWorkout(WorkoutDto model)
        {
            return _service.CreateWorkout(model);
        }

        [HttpGet("/getWorkout")]
        public WorkoutDto GetWorkout(string id)
        {
            return _service.GetWorkoutById(id);
        }

        [HttpGet("/getAllWorkouts")]
        public List<WorkoutDto> GetAllWorkouts(string id)
        {
            return _service.GetAllWorkouts();
        }

        [HttpPut("/updateWorkout")]
        public WorkoutDto UpdateWorkout(WorkoutDto model)
        {
            return _service.UpdateWorkout(model);
        }

        [HttpDelete("/deleteWorkout")]
        public void DeleteWorkout(string id)
        {
            _service.DeleteWorkout(id);
        }
    }
}
