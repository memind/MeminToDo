import { Food } from "../food/food";

export class Meal {
    id: String;
    userId: String;
    status: number;
    createdDate: Date;
    updatedDate: Date | null | undefined;
    deletedDate: Date | null | undefined;
    mealType: number;
    totalCalorie: number;
    foods: Food[];
}