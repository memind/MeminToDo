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

        public ExerciseController(IExerciseService service) => _service = service;
        

        [HttpPost]
        [Authorize(Policy = "WorkoutWrite")]
        public ExerciseDto CreateExercise(ExerciseDto model) => _service.CreateExercise(model);

        [HttpGet("/{id}")]
        [Authorize(Policy = "WorkoutRead")]
        public ExerciseDto GetExercise(string id) => _service.GetExerciseById(id);

        [HttpGet]
        [Authorize(Policy = "WorkoutRead")]
        public List<ExerciseDto> GetAllExercises() => _service.GetAllExercises();

        [HttpGet("/user/{id}")]
        [Authorize(Policy = "WorkoutRead")]
        public List<ExerciseDto> GetUsersAllExercises(string id) => _service.GetUsersAllExercises(id);

        [HttpPut]
        [Authorize(Policy = "WorkoutWrite")]
        public ExerciseDto UpdateExercise(ExerciseDto model, string id) => _service.UpdateExercise(model, id);

        [HttpDelete]
        [Authorize(Policy = "WorkoutWrite")]
        public void DeleteExercise(string id) => _service.DeleteExercise(id);
    }
}
