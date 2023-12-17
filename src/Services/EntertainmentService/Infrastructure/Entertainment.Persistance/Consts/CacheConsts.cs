namespace Entertainment.API.Consts
{
    public static class CacheConsts
    {
        public static string GetBook(string id) => $"Book:{id}";
        public static string GetAllBooks() => "AllBooks";
        public static string GetUsersAllBooks(string id) => $"UsersAllBooks:{id}";


        public static string GetBookNote(string id) => $"BookNote:{id}";
        public static string GetAllBookNotes() => "AllBookNotes";
        public static string GetUsersAllBookNotes(string id) => $"UsersAllBookNotes:{id}";


        public static string GetGame(string id) => $"Game:{id}";
        public static string GetAllGames() => "AllGames";
        public static string GetUsersAllGames(string id) => $"UsersAllBookGames:{id}";


        public static string GetShow(string id) => $"Show:{id}";
        public static string GetAllShows() => "AllShows";
        public static string GetUsersAllShows(string id) => $"UsersAllBookShows:{id}";

    }
}
