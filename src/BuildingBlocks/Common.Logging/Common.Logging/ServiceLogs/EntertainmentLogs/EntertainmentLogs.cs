namespace Common.Logging.Logs.EntertainmentLogs
{
    public static class EntertainmentLogs
    {
        public static string AnErrorOccured(string errorMessage) => $"An error occured: {errorMessage}";





        public static string CreateShow(string showName, Guid userId) => $"Created Show: {showName} - {userId}";

        public static string DeleteShow(string id) => $"Deleted Show: {id}";

        public static string GetAllShows() => "Getting All Shows";

        public static string GetShowById(string id) => $"Getting Show: {id}";

        public static string GetUsersAllShows(string userId) => $"Getting All Shows Of User: {userId}";

        public static string UpdateShow(string showName, Guid userId) => $"UserID: {userId} \n Updated Show: {showName}";





        public static string CreateGame(string gameName, Guid userId) => $"Created Game: {gameName} - {userId}";

        public static string DeleteGame(string id) => $"Deleted Game: {id}";

        public static string GetAllGames() => "Getting All Games";

        public static string GetGameById(string id) => $"Getting Game: {id}";

        public static string GetUsersAllGames(string userId) => $"Getting All Games Of User: {userId}";

        public static string UpdateGame(string gameName, Guid userId) => $"UserID: {userId} \n Updated Game: {gameName}";





        public static string CreateBook(string bookName, Guid userId) => $"Created Book: {bookName} - {userId}";

        public static string DeleteBook(string id) => $"Deleted Book: {id}";

        public static string GetAllBooks() => "Getting All Books";

        public static string GetBookById(string id) => $"Getting Book: {id}";

        public static string GetUsersAllBooks(string userId) => $"Getting All Books Of User: {userId}";

        public static string UpdateBook(string bookName, Guid userId) => $"UserID: {userId} \n Updated Book: {bookName}";





        public static string CreateBookNote(string bookNoteHeader, Guid userId) => $"Created Book Note: {bookNoteHeader} - {userId}";

        public static string DeleteBookNote(string id) => $"Deleted Book Note: {id}";

        public static string GetAllBookNotes() => "Getting All Book Notes";

        public static string GetBookNoteById(string id) => $"Getting Book Note: {id}";

        public static string GetUsersAllBookNotes(string userId) => $"Getting All Book Notes Of User: {userId}";

        public static string UpdateBookNote(string bookNoteHeader, Guid userId) => $"UserID: {userId} \n Updated Book Note: {bookNoteHeader}";
    }
}
