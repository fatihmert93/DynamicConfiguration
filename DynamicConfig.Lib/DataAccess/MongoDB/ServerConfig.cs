namespace DynamicConfig.Lib.DataAccess.MongoDB
{
    public class ServerConfig
    {
        public MongoDbConfig MongoDB { get; set; } = new MongoDbConfig();
    }
}