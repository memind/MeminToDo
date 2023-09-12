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
        public ExerciseDto CreateExercise(ExerciseDto model)
        {
            return _service.CreateExercise(model);
        }

        [HttpGet("/getExercise")]
        public ExerciseDto GetExercise(string id)
        {
            return _service.GetExerciseById(id);
        }

        [HttpGet("/getAllExercises")]
        public List<ExerciseDto> GetAllExercises()
        {
            return _service.GetAllExercises();
        }

        [HttpGet("/getUsersAllExercises")]
        public List<ExerciseDto> GetUsersAllExercises(string id)
        {
            return _service.GetUsersAllExercises(id);
        }

        [HttpPut("/updateExercise")]
        public ExerciseDto UpdateExercise(ExerciseDto model, string id)
        {
            return _service.UpdateExercise(model, id);
        }

        [HttpDelete("/deleteExercise")]
        public void DeleteExercise(string id)
        {
            _service.DeleteExercise(id);
        }
    }
}
