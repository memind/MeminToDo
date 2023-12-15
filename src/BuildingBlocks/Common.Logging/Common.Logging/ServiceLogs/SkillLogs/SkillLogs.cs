using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging.Logs.SkillLogs
{
    public static class SkillLogs
    {
        public static string AnErrorOccured(string errorMessage) => $"An error occured: {errorMessage}";





        public static string CreateSong(string songName) => $"Created Song: {songName}";
        public static string DeleteSong(string id) => $"Deleted Song: {id}";
        public static string GetAllSongs() => "Getting All Songs";
        public static string GetUsersAllSongs(Guid userId) => $"Getting All Songs Of User: {userId}";
        public static string GetSongById(string id) => $"Getting Song: {id}";
        public static string UpdateSong(string id) => $"Updated Song: {id}";





        public static string CreateArt(string artName) => $"Created Art: {artName}";
        public static string DeleteArt(string id) => $"Deleted Art: {id}";
        public static string GetAllArts() => "Getting All Arts";
        public static string GetUsersAllArts(Guid userId) => $"Getting All Arts Of User: {userId}";
        public static string GetArtById(string id) => $"Getting Art: {id}";
        public static string UpdateArt(string id) => $"Updated Art: {id}";
    }
}
