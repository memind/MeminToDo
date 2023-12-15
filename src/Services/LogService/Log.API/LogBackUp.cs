using Amazon.DynamoDBv2.DataModel;

namespace Log.API
{
    [DynamoDBTable("LogBackUps")]
    public class LogBackUp
    {
        [DynamoDBHashKey("Id")]
        public Guid? Id { get; set; }

        [DynamoDBProperty("LogMessage")]
        public string LogMessage { get; set; }

        [DynamoDBProperty("LogTime")]
        public DateTime LogTime { get; set; }

        [DynamoDBProperty("IsBackedUp")]
        public bool IsBackedUp { get; set; }

        [DynamoDBProperty("Application")]
        public string Application { get; set; }

        [DynamoDBProperty("Level")]
        public string Level { get; set; }
    }
}
