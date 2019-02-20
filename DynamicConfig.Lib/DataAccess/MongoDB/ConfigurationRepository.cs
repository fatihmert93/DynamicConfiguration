using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DynamicConfig.Lib.IOC;
using DynamicConfig.Lib.Models.Entities;
using MongoDB.Driver;

namespace DynamicConfig.Lib.DataAccess.MongoDB
{
    public class MongoConfigurationRepository : IConfigurationRepository
    {
        private readonly IConfigurationContext _context;

        public MongoConfigurationRepository()
        {
            _context = DependencyService.Instance.CurrentResolver.Resolve<IConfigurationContext>();
        }
        
        
        public async Task<IEnumerable<Configuration>> GetAll()
        {
            return await _context.Configurations
                .Find(_ => true)
                .ToListAsync();
        }

        public Task<Configuration> GetOne(long id)
        {
            FilterDefinition<Configuration> filter = Builders<Configuration>.Filter.Eq(m => m.Id, id);
            return _context
                .Configurations
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task Create(Configuration model)
        {
            await _context.Configurations.InsertOneAsync(model);
        }

        public async Task<bool> Delete(long id)
        {
            FilterDefinition<Configuration> filter = Builders<Configuration>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                .Configurations
                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                   && deleteResult.DeletedCount > 0;
        }

        public Task<long> GetNextId()
        {
            throw new System.NotImplementedException();
        }
    }
}