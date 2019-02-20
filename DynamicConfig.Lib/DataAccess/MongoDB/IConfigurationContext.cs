using DynamicConfig.Lib.Models.Entities;
using MongoDB.Driver;

namespace DynamicConfig.Lib.DataAccess.MongoDB
{
    public interface IConfigurationContext
    {
        IMongoCollection<Configuration> Configurations { get; }
    }
}