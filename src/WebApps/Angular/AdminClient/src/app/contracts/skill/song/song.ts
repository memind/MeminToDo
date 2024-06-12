export class Song {
    id: String;
    userId: String;
    createdDate: Date;
    updatedDate: Date | null | undefined;
    songName: String;
    artistName: String;
    instrument: number;
}