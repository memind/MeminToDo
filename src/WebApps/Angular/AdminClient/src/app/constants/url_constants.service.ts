import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UrlConstantsService {
  readonly AGGREGATOR_URL = 'https://localhost:8020';

  // Budget API Endpoints
  readonly BUDGET_BASE = `${this.AGGREGATOR_URL}/Budget`;
  readonly BUDGET_API = `${this.AGGREGATOR_URL}/api/budget`;

  getAllBudgetAccountsApi(budgetAccountId: number) {
    return `${this.BUDGET_API}/${budgetAccountId}`;
  }

  getUsersBudgetAccountsApi(userId: number) {
    return `${this.BUDGET_API}/useraccounts/${userId}`;
  }

  // Wallet API Endpoints
  readonly WALLET_BASE = `${this.AGGREGATOR_URL}/Wallet`;
  readonly WALLET_API = `${this.AGGREGATOR_URL}/api/wallet`;

  getWalletApi(walletId: number) {
    return `${this.WALLET_API}/${walletId}`;
  }

  getUsersWalletsApi(userId: number) {
    return `${this.WALLET_API}/userwallets/${userId}`;
  }

  // MoneyFlow API Endpoints
  readonly FLOW_BASE = `${this.AGGREGATOR_URL}/Flow`;
  readonly FLOW_API = `${this.AGGREGATOR_URL}/api/moneyflow`;

  getMoneyFlowApi(moneyFlowId: number) {
    return `${this.FLOW_API}/${moneyFlowId}`;
  }

  getUsersMoneyFlowsApi(userId: number) {
    return `${this.FLOW_API}/userflows/${userId}`;
  }

  // Workout API Endpoints
  readonly WORKOUT_BASE = `${this.AGGREGATOR_URL}/Workout`;
  readonly WORKOUT_API = `${this.AGGREGATOR_URL}/api/workout`;

  getWorkoutApi(workoutId: number) {
    return `${this.AGGREGATOR_URL}/api/workout/${workoutId}`;
  }

  getUsersWorkoutsApi(userId: number) {
    return `${this.AGGREGATOR_URL}/api/workout/user/${userId}`;
  }

  // Exercise API Endpoints
  readonly EXERCISE_BASE = `${this.AGGREGATOR_URL}/Exercise`;
  readonly EXERCISE_API = `${this.AGGREGATOR_URL}/api/exercise`;

  getExerciseApi(exerciseId: number) {
    return `${this.AGGREGATOR_URL}/api/exercise/${exerciseId}`;
  }

  getUsersExercisesApi(userId: number) {
    return `${this.AGGREGATOR_URL}/api/exercise/user/${userId}`;
  }

  // Entertainment API Endpoints
  readonly ENTERTAINMENT_BASE = `${this.AGGREGATOR_URL}/Entertainment`;
  readonly SHOW_API = `${this.AGGREGATOR_URL}/api/show`;
  readonly BOOKNOTE_API = `${this.AGGREGATOR_URL}/api/booknote`;
  readonly GAME_API = `${this.AGGREGATOR_URL}/api/game`;
  readonly BOOK_API = `${this.AGGREGATOR_URL}/api/book`;

  getShowApi(showId: number) {
    return `${this.SHOW_API}/${showId}`;
  }

  getUsersShowsApi(userId: number) {
    return `${this.SHOW_API}/user/${userId}`;
  }

  getGameApi(gameId: number) {
    return `${this.GAME_API}/${gameId}`;
  }

  getUsersGamesApi(userId: number) {
    return `${this.GAME_API}/user/${userId}`;
  }

  getBookNoteApi(bookNoteId: number) {
    return `${this.BOOKNOTE_API}/${bookNoteId}`;
  }

  getUsersBookNotesApi(userId: number) {
    return `${this.BOOKNOTE_API}/user/${userId}`;
  }

  getBookApi(bookId: number) {
    return `${this.BOOK_API}/${bookId}`;
  }

  getUsersBooksApi(userId: number) {
    return `${this.BOOK_API}/user/${userId}`;
  }

  // Skill API Endpoints
  readonly SKILL_BASE = `${this.AGGREGATOR_URL}/Skill`;
  readonly ART_API = `${this.AGGREGATOR_URL}/api/art`;
  readonly SONG_API = `${this.AGGREGATOR_URL}/api/song`;

  getArtApi(ArtId: number) {
    return `${this.ART_API}/getoneart?id=${ArtId}`;
  }

  getUsersArtsApi(userId: number) {
    return `${this.ART_API}/getusersallarts?id=${userId}`;
  }

  getSongApi(songId: number) {
    return `${this.SONG_API}/getonesong?id=${songId}`;
  }

  getUsersSongsApi(userId: number) {
    return `${this.SONG_API}/getusersallsongs?id=${userId}`;
  }

  getSongUploadApi(id: number, path: string) {
    return `${this.SONG_API}upload?id=${id}&path=${path}`;
  }

  getSongDownloadApi(fileId: number) {
    return `${this.SONG_API}/download/${fileId}`;
  }

  getArtFileApi(fileId: number) {
    return `${this.ART_API}/file/${fileId}`;
  }

  // Meal API Endpoints
  readonly MEAL_BASE = `${this.AGGREGATOR_URL}/Meal`;
  readonly FOOD_API = `${this.AGGREGATOR_URL}/api/food`;
  readonly FOOD_HARDDELETE_API = `${this.FOOD_API}/harddelete`;
  readonly FOOD_DELETED_API = `${this.AGGREGATOR_URL}/api/food/deleted`;
  readonly FOOD_ACTIVE_API = `${this.AGGREGATOR_URL}/api/food/active`;
  readonly MEAL_API = `${this.AGGREGATOR_URL}/api/meal`;
  readonly MEAL_HARDDELETE_API = `${this.MEAL_API}/harddelete`;
  readonly MEAL_ACTIVE_API = `${this.MEAL_API}/active`;
  readonly MEAL_DELETED_API = `${this.MEAL_API}/deleted`;

  getFoodsInMealApi(mealId: number) {
    return `${this.FOOD_API}/meal/${mealId}`;
  }

  getFoodApi(foodId: number) {
    return `${this.FOOD_API}/${foodId}`;
  }

  getUsersFoodsApi(userId: number) {
    return `${this.FOOD_API}/user/${userId}`;
  }

  getMealApi(mealId: number) {
    return `${this.MEAL_API}/${mealId}`;
  }

  getUsersMealsApi(userId: number) {
    return `${this.MEAL_API}/user/${userId}`;
  }

  // Dashboard Aggregator Endpoints
  readonly DASHBOARD_ADMIN_API = `${this.AGGREGATOR_URL}/GetAdminDashboardInfos`;

  getUserDashboardApi(userId: number) {
    return `${this.AGGREGATOR_URL}/GetUserDashboardInfos?id=${userId}`;
  }

  constructor() { }
}
