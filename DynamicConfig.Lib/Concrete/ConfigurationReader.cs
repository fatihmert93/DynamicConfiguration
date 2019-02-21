using System;
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
    public static class ConfigSettings
    {
        public static string ApplicationName { get; set; } = "ServiceA";
        public static string ConnectionString { get; set; } = "";
        public static int RefreshTimerIntervalInMs { get; set; } = 5000;
    }
    
    public class ConfigurationReader : IConfigurationReader
    {
        public string ApplicationName { get; set; }
        private AutoResetEvent _autoEvent = null;
        private Timer _timer = null;
        private ICollection<Configuration> _confList;

        private readonly IConfigurationRepository _configurationRepository;

        private readonly int _refreshTimerInterval;
        private readonly string _applicationName;
        private readonly string _connectionString;

        public CancellationToken CancellationToken { get; set; }

        public ConfigurationReader(string applicationName,string connectionString,int refreshTimerIntervalInMs)
        {
            _configurationRepository = DependencyService.Instance.CurrentResolver.Resolve<IConfigurationRepository>();
            _confList = new List<Configuration>();
            _refreshTimerInterval = refreshTimerIntervalInMs;
            _applicationName = applicationName;
            _connectionString = connectionString;

            Task.Run(() => this.CheckDatas()).Wait(); // first call for config datas
            Task.Run(() => this.StartTimer(CancellationToken));
        }

        public async Task CheckDatas()
        {
            Console.WriteLine("triggered");

            IEnumerable<Configuration> list = await _configurationRepository.GetAll();
            List<Configuration> conflist = list.ToList();
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
                    // do the work in the loop
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

    internal class DataTypeMapper
    {
        public static Dictionary<Type, string> DataMapper()
        {
            Dictionary<Type, String> dataMapper = new Dictionary<Type, string>();
            dataMapper.Add(typeof(int), "int");
            dataMapper.Add(typeof(int?),"integer null");
            dataMapper.Add(typeof(string), "string");
            dataMapper.Add(typeof(bool), "boolean");
            dataMapper.Add(typeof(bool?),"boolean null");
            dataMapper.Add(typeof(DateTime), "datetime");
            dataMapper.Add(typeof(DateTime?),"datetime null");
            dataMapper.Add(typeof(float), "float");
            dataMapper.Add(typeof(float?),"float null");
            dataMapper.Add(typeof(decimal), "decimal");
            dataMapper.Add(typeof(decimal?),"decimal null");
            dataMapper.Add(typeof(long),"long");
            dataMapper.Add(typeof(long?),"long null");
            dataMapper.Add(typeof(byte[]),"bytea");
            dataMapper.Add(typeof(TimeSpan),"timespan");
            dataMapper.Add(typeof(TimeSpan?),"timespan null");
            dataMapper.Add(typeof(Guid), "guid");
            dataMapper.Add(typeof(Guid?),"guid null");
            return dataMapper;
        }

        public static Dictionary<string, Type> MapperWithKey()
        {
            Dictionary<string,Type> datamapper = new Dictionary<string, Type>();
            datamapper.Add("int",typeof(int));
            datamapper.Add("integer null",typeof(int?));
            datamapper.Add("string",typeof(string));
            datamapper.Add("boolean",typeof(bool));
            datamapper.Add("boolean null",typeof(bool?));
            datamapper.Add("datetime",typeof(DateTime));
            datamapper.Add("datetime null",typeof(DateTime?));
            datamapper.Add("float",typeof(float));
            datamapper.Add("float null",typeof(float?));
            return datamapper;
        }
    }
}