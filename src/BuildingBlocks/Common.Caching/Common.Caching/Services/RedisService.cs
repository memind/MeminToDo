using Common.Caching.Consts;
using StackExchange.Redis;
using System.Net;
using System.Net.Sockets;

namespace Common.Caching.Services
{
    public static class RedisService
    {
        static ConfigurationOptions sentinelOptions => new()
        {
            EndPoints =
            {
                {RedisConsts.GetLocalhost(), RedisConsts.GetFirstSentinelsPort() },
                {RedisConsts.GetLocalhost(), RedisConsts.GetSecondSentinelsPort() }
            },
            CommandMap = CommandMap.Sentinel,
            AbortOnConnectFail = false
        };


        static ConfigurationOptions masterOptions => new()
        {
            AbortOnConnectFail = false
        };

        public static IDatabase GetRedisMasterDatabase()
        {
            ConnectionMultiplexer sentinelConnection = ConnectionMultiplexer.SentinelConnect(sentinelOptions);
            EndPoint masterEndpoint = null;

            foreach (EndPoint endpoint in sentinelConnection.GetEndPoints())
            {
                var endString = endpoint.ToString().Remove(0,12);
                IServer server = sentinelConnection.GetServer(endString);

                if (!server.IsConnected)
                    continue;

                masterEndpoint = server.SentinelGetMasterAddressByName(RedisConsts.GetSentinelConfigurationMasterName());
                break;
            }

            string localMasterIP = null;

            if (masterEndpoint.ToString() == RedisConsts.GetRedisMastersIP())
                localMasterIP = RedisConsts.GetLocalEndPointOfMaster();

            if (masterEndpoint.ToString() == RedisConsts.GetRedisFirstSlavesIP())
                localMasterIP = RedisConsts.GetLocalEndPointOfFirstSlave();

            if (masterEndpoint.ToString() == RedisConsts.GetRedisSecondSlavesIP())
                localMasterIP = RedisConsts.GetLocalEndPointOfSecondSlave();

            if (masterEndpoint.ToString() == RedisConsts.GetRedisThirdSlavesIP())
                localMasterIP = RedisConsts.GetLocalEndPointOfThirdSlave();

            ConnectionMultiplexer masterConnection = ConnectionMultiplexer.Connect(localMasterIP);

            IDatabase database = masterConnection.GetDatabase();
            return database;
        }
    }
}
