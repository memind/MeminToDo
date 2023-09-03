using AutoMapper;
using Workout.Application.DTOs.ExerciseDTOs;
using Workout.Domain.Entities;

namespace Workout.Application.Profiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<Exercise, ExerciseDto>().ReverseMap();
        }
    }
}
