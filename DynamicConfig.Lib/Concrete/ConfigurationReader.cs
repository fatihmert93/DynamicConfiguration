using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using DynamicConfig.Lib.Abstract;
using DynamicConfig.Lib.DataAccess.MongoDB;
using DynamicConfig.Lib.IOC;
using DynamicConfig.Lib.Models.Entities;
using MongoDB.Driver.Core.Configuration;

namespace DynamicConfig.Lib.Concrete
{
    
    public class ConfigurationReader : IConfigurationReader
    {
        private ConcurrentBag<Configuration> _confList;

        private readonly IConfigurationRepository _configurationRepository;

        private readonly int _refreshTimerInterval;
        private readonly string _applicationName;
        private readonly string _connectionString;
       

        public CancellationToken CancellationToken { get; set; }

        public ConfigurationReader(string applicationName,string connectionString,int refreshTimerIntervalInMs)
        {
            _configurationRepository = DependencyService.Instance.CurrentResolver.Resolve<IConfigurationRepository>();
            _confList = new ConcurrentBag<Configuration>();
            _refreshTimerInterval = refreshTimerIntervalInMs;
            _applicationName = applicationName;
            _connectionString = connectionString;

            Task.Run(() => this.CheckDatas()).Wait(); // first call for config datas
            Task.Run(() => this.StartTimer(CancellationToken));
        }

        public async Task CheckDatas()
        {
            IEnumerable<Configuration> list = await _configurationRepository.GetAll();
            List<Configuration> conflist = list.Where(v => v.ApplicationName == _applicationName && v.IsActive == true).ToList();
            _confList.Clear();
            foreach (Configuration configuration in list)
            {
                _confList.Add(configuration);
            }
        }

        private async Task StartTimer(CancellationToken cancellationToken)
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    await CheckDatas();
                    await Task.Delay(_refreshTimerInterval, cancellationToken);
                    if (cancellationToken.IsCancellationRequested)
                        break;
                }
            }, cancellationToken);
        }
        
        public T GetValue<T>(string key)
        {
            Configuration configuration = _confList.FirstOrDefault(v => v.Name == key);
            if(configuration == null)
                throw new ArgumentNullException("There is no value for this key");

            Type confType = TypeMap(configuration.Type);
            
            object value = Convert.ChangeType(configuration.Value, confType);
            return (T) value;
        }

        public Type TypeMap(string type)
        {
            return DataTypeMapper.MapperWithKey()[type];
        }
    }

    
}