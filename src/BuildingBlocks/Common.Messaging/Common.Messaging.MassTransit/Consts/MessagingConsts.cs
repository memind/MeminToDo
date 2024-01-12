namespace Common.Messaging.MassTransit.Consts
{
    public static class MessagingConsts
    {
        public static string GetConnectedInfoQueue() => "connected-infos";
        public static string StartTestQueue() => "test-queue";
        public static string BackUpQueue() => "back-up-queue";
        public static string GetConnectedInfo(string serviceName) => $"{serviceName} is connected to Mass Transit!";
        public static string SendBackUpInfo() => $"{DateTime.UtcNow.Date} Backing up Logs...";
        public static string StartTest(int count) => $"Message Test {count}) {DateTime.UtcNow}";
    }
}
