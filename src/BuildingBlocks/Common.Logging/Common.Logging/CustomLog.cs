namespace Common.Logging
{
    public class CustomLog
    {
        public string LogMessage { get; set; }
        public DateTime LogTime { get; set; }
        public bool IsBackedUp { get; set; }
        public string Application { get; set; }
        public string Level { get; set; }
    }
}
