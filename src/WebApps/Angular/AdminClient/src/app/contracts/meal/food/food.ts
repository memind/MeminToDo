import { Meal } from "../meal/meal";

export class Food {
    id: String;
    userId: String;
    status: number;
    createdDate: Date;
    updatedDate: Date | null | undefined;
    deletedDate: Date | null | undefined;
    name: String;
    calorieByServing: number;
    category: number;
    meals: Meal[];
}