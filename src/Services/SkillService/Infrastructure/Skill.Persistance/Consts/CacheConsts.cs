namespace Skill.Persistance.Consts
{
    public static class CacheConsts
    {
        public static string GetAllArts() => "GetAllArts";
        public static string GetArtById(string id) => $"GetArtById:{id}";
        public static string GetUsersAllArts(Guid id) => $"GetUsersAllArts:{id}";


        public static string GetAllSongs() => "GetAllSongs";
        public static string GetSongById(string id) => $"GetSongById:{id}";
        public static string GetUsersAllSongs(Guid id) => $"GetUsersAllSongs:{id}";
    }
}
