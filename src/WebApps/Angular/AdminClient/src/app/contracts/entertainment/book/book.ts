import { BookNote } from "../book-note/book_note";

export class Book {
    userId: String;
    createdDate: Date;
    updatedDate: Date;
    bookName: String;
    authorName: String;
    pageCount: number;
    isFinished: boolean;
    bookNotes: BookNote[] | undefined | null;
}