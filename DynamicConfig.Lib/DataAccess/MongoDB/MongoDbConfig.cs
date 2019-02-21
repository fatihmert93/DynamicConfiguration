using DynamicConfig.Lib.Concrete;

namespace DynamicConfig.Lib.DataAccess.MongoDB
{
    public class MongoDbConfig
    {
        public string Database { get; set; } = "ConfigurationDB";
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 27017;
        public string User { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConnectionString 
        {
            get 
            {
                if (!string.IsNullOrEmpty(ConfigSettings.ConnectionString))
                {
                    return ConfigSettings.ConnectionString;
                }
                if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                    return $@"mongodb://{Host}:{Port}";
                return $@"mongodb://{User}:{Password}@{Host}:{Port}";
            }
        }
    }
}