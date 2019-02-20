using Autofac;
using DynamicConfig.Lib.IOC;
using DynamicConfig.Lib.Models.Entities;
using MongoDB.Driver;

namespace DynamicConfig.Lib.DataAccess.MongoDB
{
    public class ConfigurationContext : IConfigurationContext
    {
        private readonly IMongoDatabase _db;

        public ConfigurationContext()
        {
            MongoDbConfig config = DependencyService.Instance.CurrentResolver.Resolve<MongoDbConfig>();
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }


        public IMongoCollection<Configuration> Configurations => _db.GetCollection<Configuration>("Configurations");
    }
}