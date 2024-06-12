import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationConstantsService {
  readonly CREATED_SUCCESSFULLY = 'created successfully!';
  readonly UPDATED_SUCCESSFULLY = 'updated successfully!';
  readonly DELETED_SUCCESSFULLY = 'deleted successfully!';
  readonly HARD_DELETED_SUCCESSFULLY = 'hard deleted successfully!';
  
  readonly CREATED_ERROR = 'An error occured while creating';
  readonly UPDATED_ERROR = 'An error occured while updating';
  readonly DELETED_ERROR = 'An error occured while deleting';
  readonly HARD_DELETED_ERROR = 'An error occured while hard deleting';

  // --------------------------------- WORKOUT --------------------------------- 
  getExerciseCreatedSuccessfullyMessage() {
    return `Exercise have been ${this.CREATED_SUCCESSFULLY}`;
  }

  getExerciseUpdatedSuccessfullyMessage() {
    return `Exercise have been ${this.UPDATED_SUCCESSFULLY}`;
  }

  getExerciseDeletedSuccessfullyMessage() {
    return `Exercise have been ${this.DELETED_SUCCESSFULLY}`;
  }

  getExerciseCreatedErrorMessage() {
    return `${this.CREATED_ERROR} exercise!`;
  }

  getExerciseUpdatedErrorMessage() {
    return `${this.UPDATED_ERROR} exercise!`;
  }

  getExerciseDeletedErrorMessage() {
    return `${this.DELETED_ERROR} exercise!`;
  }

// --------------------------------- MEAL --------------------------------- 
  getMealCreatedSuccessfullyMessage() {
    return `Meal have been ${this.CREATED_SUCCESSFULLY}`;
  }

  getMealUpdatedSuccessfullyMessage() {
    return `Meal have been ${this.UPDATED_SUCCESSFULLY}`;
  }

  getMealDeletedSuccessfullyMessage() {
    return `Meal have been ${this.DELETED_SUCCESSFULLY}`;
  }

  getMealCreatedErrorMessage() {
    return `${this.CREATED_ERROR} Meal!`;
  }

  getMealUpdatedErrorMessage() {
    return `${this.UPDATED_ERROR} Meal!`;
  }

  getMealDeletedErrorMessage() {
    return `${this.DELETED_ERROR} Meal!`;
  }

  getFoodCreatedSuccessfullyMessage() {
    return `Food have been ${this.CREATED_SUCCESSFULLY}`;
  }

  getFoodUpdatedSuccessfullyMessage() {
    return `Food have been ${this.UPDATED_SUCCESSFULLY}`;
  }

  getFoodDeletedSuccessfullyMessage() {
    return `Food have been ${this.DELETED_SUCCESSFULLY}`;
  }

  getFoodHardDeletedSuccessfullyMessage() {
    return `Food have been ${this.HARD_DELETED_SUCCESSFULLY}`;
  }

  getFoodCreatedErrorMessage() {
    return `${this.CREATED_ERROR} Food!`;
  }

  getFoodUpdatedErrorMessage() {
    return `${this.UPDATED_ERROR} Food!`;
  }

  getFoodDeletedErrorMessage() {
    return `${this.DELETED_ERROR} Food!`;
  }

  getFoodHardDeletedErrorMessage() {
    return `${this.HARD_DELETED_ERROR} Food!`;
  }

// --------------------------------- ENTERTAINMENT --------------------------------- 
  getBookCreatedSuccessfullyMessage() {
    return `Book have been ${this.CREATED_SUCCESSFULLY}`;
  }

  getBookUpdatedSuccessfullyMessage() {
    return `Book have been ${this.UPDATED_SUCCESSFULLY}`;
  }

  getBookDeletedSuccessfullyMessage() {
    return `Book have been ${this.DELETED_SUCCESSFULLY}`;
  }

  getBookCreatedErrorMessage() {
    return `${this.CREATED_ERROR} Book!`;
  }

  getBookUpdatedErrorMessage() {
    return `${this.UPDATED_ERROR} Book!`;
  }

  getBookDeletedErrorMessage() {
    return `${this.DELETED_ERROR} Book!`;
  }

  getGameCreatedSuccessfullyMessage() {
    return `Game have been ${this.CREATED_SUCCESSFULLY}`;
  }

  getGameUpdatedSuccessfullyMessage() {
    return `Game have been ${this.UPDATED_SUCCESSFULLY}`;
  }

  getGameDeletedSuccessfullyMessage() {
    return `Game have been ${this.DELETED_SUCCESSFULLY}`;
  }

  getGameCreatedErrorMessage() {
    return `${this.CREATED_ERROR} Game!`;
  }

  getGameUpdatedErrorMessage() {
    return `${this.UPDATED_ERROR} Game!`;
  }

  getGameDeletedErrorMessage() {
    return `${this.DELETED_ERROR} Game!`;
  }

  getShowCreatedSuccessfullyMessage() {
    return `Show have been ${this.CREATED_SUCCESSFULLY}`;
  }

  getShowUpdatedSuccessfullyMessage() {
    return `Show have been ${this.UPDATED_SUCCESSFULLY}`;
  }

  getShowDeletedSuccessfullyMessage() {
    return `Show have been ${this.DELETED_SUCCESSFULLY}`;
  }

  getShowCreatedErrorMessage() {
    return `${this.CREATED_ERROR} Show!`;
  }

  getShowUpdatedErrorMessage() {
    return `${this.UPDATED_ERROR} Show!`;
  }

  getShowDeletedErrorMessage() {
    return `${this.DELETED_ERROR} Show!`;
  }

  getBookNoteCreatedSuccessfullyMessage() {
    return `Book Note have been ${this.CREATED_SUCCESSFULLY}`;
  }

  getBookNoteUpdatedSuccessfullyMessage() {
    return `Book Note have been ${this.UPDATED_SUCCESSFULLY}`;
  }

  getBookNoteDeletedSuccessfullyMessage() {
    return `Book Note have been ${this.DELETED_SUCCESSFULLY}`;
  }

  getBookNoteCreatedErrorMessage() {
    return `${this.CREATED_ERROR} Book Note!`;
  }

  getBookNoteUpdatedErrorMessage() {
    return `${this.UPDATED_ERROR} Book Note!`;
  }

  getBookNoteDeletedErrorMessage() {
    return `${this.DELETED_ERROR} Book Note!`;
  }

// --------------------------------- BUDGET --------------------------------- 
  getBudgetAccountCreatedSuccessfullyMessage() {
    return `Budget Account have been ${this.CREATED_SUCCESSFULLY}`;
  }

  getBudgetAccountUpdatedSuccessfullyMessage() {
    return `Budget Account have been ${this.UPDATED_SUCCESSFULLY}`;
  }

  getBudgetAccountDeletedSuccessfullyMessage() {
    return `Budget Account have been ${this.DELETED_SUCCESSFULLY}`;
  }

  getBudgetAccountCreatedErrorMessage() {
    return `${this.CREATED_ERROR} Budget Account!`;
  }

  getBudgetAccountUpdatedErrorMessage() {
    return `${this.UPDATED_ERROR} Budget Account!`;
  }

  getBudgetAccountDeletedErrorMessage() {
    return `${this.DELETED_ERROR} Budget Account!`;
  }

  getMoneyFlowCreatedSuccessfullyMessage() {
    return `Money Flow have been ${this.CREATED_SUCCESSFULLY}`;
  }

  getMoneyFlowUpdatedSuccessfullyMessage() {
    return `Money Flow have been ${this.UPDATED_SUCCESSFULLY}`;
  }

  getMoneyFlowDeletedSuccessfullyMessage() {
    return `Money Flow have been ${this.DELETED_SUCCESSFULLY}`;
  }

  getMoneyFlowCreatedErrorMessage() {
    return `${this.CREATED_ERROR} Money Flow!`;
  }

  getMoneyFlowUpdatedErrorMessage() {
    return `${this.UPDATED_ERROR} Money Flow!`;
  }

  getMoneyFlowDeletedErrorMessage() {
    return `${this.DELETED_ERROR} Money Flow!`;
  }

  getWalletCreatedSuccessfullyMessage() {
    return `Wallet have been ${this.CREATED_SUCCESSFULLY}`;
  }

  getWalletUpdatedSuccessfullyMessage() {
    return `Wallet have been ${this.UPDATED_SUCCESSFULLY}`;
  }

  getWalletDeletedSuccessfullyMessage() {
    return `Wallet have been ${this.DELETED_SUCCESSFULLY}`;
  }

  getWalletCreatedErrorMessage() {
    return `${this.CREATED_ERROR} Wallet!`;
  }

  getWalletUpdatedErrorMessage() {
    return `${this.UPDATED_ERROR} Wallet!`;
  }

  getWalletDeletedErrorMessage() {
    return `${this.DELETED_ERROR} Wallet!`;
  }

// --------------------------------- SKILL --------------------------------- 
  getArtCreatedSuccessfullyMessage() {
    return `Art have been ${this.CREATED_SUCCESSFULLY}`;
  }

  getArtUpdatedSuccessfullyMessage() {
    return `Art have been ${this.UPDATED_SUCCESSFULLY}`;
  }

  getArtDeletedSuccessfullyMessage() {
    return `Art have been ${this.DELETED_SUCCESSFULLY}`;
  }

  getArtCreatedErrorMessage() {
    return `${this.CREATED_ERROR} Art!`;
  }

  getArtUpdatedErrorMessage() {
    return `${this.UPDATED_ERROR} Art!`;
  }

  getArtDeletedErrorMessage() {
    return `${this.DELETED_ERROR} Art!`;
  }

  getSongCreatedSuccessfullyMessage() {
    return `Song have been ${this.CREATED_SUCCESSFULLY}`;
  }

  getSongUpdatedSuccessfullyMessage() {
    return `Song have been ${this.UPDATED_SUCCESSFULLY}`;
  }

  getSongDeletedSuccessfullyMessage() {
    return `Song have been ${this.DELETED_SUCCESSFULLY}`;
  }

  getSongCreatedErrorMessage() {
    return `${this.CREATED_ERROR} Song!`;
  }

  getSongUpdatedErrorMessage() {
    return `${this.UPDATED_ERROR} Song!`;
  }

  getSongDeletedErrorMessage() {
    return `${this.DELETED_ERROR} Song!`;
  }

  constructor() { }
}
