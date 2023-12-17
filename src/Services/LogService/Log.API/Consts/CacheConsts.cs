namespace Log.API.Consts
{
    public static class CacheConsts
    {
        public static string GetById(Guid logId) => $"LogBackUp:{logId}";
        public static string GetAllLogs() => "AllLogs";
    }
}
