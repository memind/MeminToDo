using Dashboard.Aggregator.Models;
using Dashboard.Aggregator.Services.Abstractions;

namespace Dashboard.Aggregator.Services.Concretes
{
    public class DashboardService : IDashboardService
    {
        private readonly IEntertainmentService _entertainmentService;
        private readonly ISkillService _skillService;
        private readonly IWorkoutService _workoutService;

        public DashboardService(IEntertainmentService entertainmentService, ISkillService skillService, IWorkoutService workoutService)
        {
            _entertainmentService = entertainmentService;
            _skillService = skillService;
            _workoutService = workoutService;
        }

        public async Task<AdminDashboardModel> GetAdminDashboardInfos()
        {
            var infos = new AdminDashboardModel()
            {
                BookCount = await _entertainmentService.GetTotalBookCount(),
                ShowCount = await _entertainmentService.GetTotalShowCount(),
                GameCount = await _entertainmentService.GetTotalGameCount(),
                BookNoteCount = await _entertainmentService.GetTotalBookNoteCount(),

                ArtCount = await _skillService.GetTotalArtCount(),
                SongCount = await _skillService.GetTotalSongCount(),

                WorkoutCount = await _workoutService.GetTotalWorkoutCount(),
                ExerciseCount = await _workoutService.GetTotalExerciseCount(),
            };

            return infos;
        }

        public async Task<UserDashboardModel> GetUserDashboardInfos(string id)
        {
            var infos = new UserDashboardModel()
            {
                MyBooksCount = await _entertainmentService.GetUserBookCount(id),
                MyShowsCount = await _entertainmentService.GetUserShowCount(id),
                MyGamesCount = await _entertainmentService.GetUserGameCount(id),
                MyBookNotesCount = await _entertainmentService.GetUserBookNoteCount(id),

                MyArtsCount = await _skillService.GetUsersArtCount(id),
                MySongsCount = await _skillService.GetUsersSongCount(id),

                MyWorkoutsCount = await _workoutService.GetUsersWorkoutsCount(id),
                MyExercisesCount = await _workoutService.GetUsersExercisesCount(id),
            };

            return infos;
        }
    }
}
