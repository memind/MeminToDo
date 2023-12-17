using System.Net;

namespace Common.Caching.Consts
{
    public static class RedisConsts
    {
        public static string GetSentinelConfigurationMasterName() => "MeminToDoRedisMaster";

        public static string GetRedisMastersIP() => "172.18.0.2:6379";
        public static string GetRedisFirstSlavesIP() => "172.18.0.3:6379";
        public static string GetRedisSecondSlavesIP() => "172.18.0.4:6379";
        public static string GetRedisThirdSlavesIP() => "172.18.0.5:6379";

        public static string GetLocalEndPointOfMaster() => "localhost:8030";
        public static string GetLocalEndPointOfFirstSlave() => "localhost:8031";
        public static string GetLocalEndPointOfSecondSlave() => "localhost:8032";
        public static string GetLocalEndPointOfThirdSlave() => "localhost:8033";

        public static string GetLocalhost() => "localhost";
        public static int GetFirstSentinelsPort() => 8034;
        public static int GetSecondSentinelsPort() => 8035;
    }
}
