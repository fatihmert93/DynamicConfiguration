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

        public Task<Configuration> GetOne(string id)
        {
            FilterDefinition<Configuration> filter = Builders<Configuration>.Filter.Eq(m => m._id, id);
            return _context
                .Configurations
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task Create(Configuration model)
        {
            await _context.Configurations.InsertOneAsync(model);
        }

        public async Task<bool> Update(Configuration model)
        {
            ReplaceOneResult updateResult =
                await _context
                    .Configurations
                    .ReplaceOneAsync(
                        filter: g => g._id == model._id,
                        replacement: model);
            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Configuration> filter = Builders<Configuration>.Filter.Eq(m => m._id, id);
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