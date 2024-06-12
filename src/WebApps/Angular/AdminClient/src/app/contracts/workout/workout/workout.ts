import { Exercise } from "../exercise/exercise";

export class Workout {
    name: String;
    createdDate: Date;
    userId: String;
    exercises: Exercise[];
}