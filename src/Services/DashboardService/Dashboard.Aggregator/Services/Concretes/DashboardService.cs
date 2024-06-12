using Dashboard.Aggregator.Models;
using Dashboard.Aggregator.Services.Abstractions;

namespace Dashboard.Aggregator.Services.Concretes
{
    public class DashboardService : IDashboardService
    {
        private readonly IEntertainmentService _entertainmentService;
        private readonly ISkillService _skillService;
        private readonly IWorkoutService _workoutService;
        private readonly IBudgetService _budgetService;
        private readonly IMealService _mealService;

        public DashboardService(IEntertainmentService entertainmentService, ISkillService skillService, IWorkoutService workoutService, IBudgetService budgetService, IMealService mealService)
        {
            _entertainmentService = entertainmentService;
            _skillService = skillService;
            _workoutService = workoutService;
            _budgetService = budgetService;
            _mealService = mealService;
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

                FoodCount = await _mealService.GetTotalFoodCount(),
                MealCount = await _mealService.GetTotalMealCount(),

                BudgetAccountCount = await _budgetService.GetTotalBudgetAccountCount(),
                MoneyFlowCount = await _budgetService.GetTotalMoneyFlowCount(),
                WalletCount = await _budgetService.GetTotalWalletCount()
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

                MyFoodCount = await _mealService.GetUsersFoodCount(id),
                MyMealCount = await _mealService.GetUsersMealCount(id),

                MyBudgetAccountCount = await _budgetService.GetUsersBudgetAccountCount(id),
                MyMoneyFlowCount = await _budgetService.GetUsersMoneyFlowCount(id),
                MyWalletCount = await _budgetService.GetUsersWalletCount(id)
            };

            return infos;
        }
    }
}
