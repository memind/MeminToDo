using AutoMapper;
using Workout.Application.DTOs.WorkoutDTOs;
using w = Workout.Domain.Entities;

namespace Workout.Application.Profiles
{
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<w.Workout, WorkoutDto>().ReverseMap();
        }
    }
}
