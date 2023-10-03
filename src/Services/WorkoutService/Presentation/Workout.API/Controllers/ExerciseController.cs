using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workout.Application.Abstractions.Services;
using Workout.Application.DTOs.ExerciseDTOs;

namespace Workout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _service;

        public ExerciseController(IExerciseService service)
        {
            _service = service;
        }

        [HttpPost("/createExercise")]
        [Authorize(Policy = "WorkoutWrite")]
        public ExerciseDto CreateExercise(ExerciseDto model)
        {
            return _service.CreateExercise(model);
        }

        [HttpGet("/getExercise")]
        [Authorize(Policy = "WorkoutRead")]
        public ExerciseDto GetExercise(string id)
        {
            return _service.GetExerciseById(id);
        }

        [HttpGet("/getAllExercises")]
        [Authorize(Policy = "WorkoutRead")]
        public List<ExerciseDto> GetAllExercises()
        {
            return _service.GetAllExercises();
        }

        [HttpGet("/getUsersAllExercises")]
        [Authorize(Policy = "WorkoutRead")]
        public List<ExerciseDto> GetUsersAllExercises(string id)
        {
            return _service.GetUsersAllExercises(id);
        }

        [HttpPut("/updateExercise")]
        [Authorize(Policy = "WorkoutWrite")]
        public ExerciseDto UpdateExercise(ExerciseDto model, string id)
        {
            return _service.UpdateExercise(model, id);
        }

        [HttpDelete("/deleteExercise")]
        [Authorize(Policy = "WorkoutWrite")]
        public void DeleteExercise(string id)
        {
            _service.DeleteExercise(id);
        }
    }
}
