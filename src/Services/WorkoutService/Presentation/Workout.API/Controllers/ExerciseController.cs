﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workout.Application.Abstractions.Services;
using Workout.Application.DTOs.ExerciseDTOs;

namespace Workout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public List<ExerciseDto> GetAllExercises(string id)
        {
            return _service.GetAllExercises();
        }

        [HttpPut("/updateExercise")]
        public ExerciseDto UpdateExercise(ExerciseDto model)
        {
            return _service.UpdateExercise(model);
        }

        [HttpDelete("/deleteExercise")]
        public void DeleteExercise(string id)
        {
            _service.DeleteExercise(id);
        }
    }
}
