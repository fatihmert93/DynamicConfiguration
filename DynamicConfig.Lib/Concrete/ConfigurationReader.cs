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

namespace DynamicConfig.Lib.Concrete
{
    public static class ConfigSettings
    {
        public static string ApplicationName { get; set; } = "";
        public static string ConnectionString { get; set; } = "";
        public static int RefreshTimerIntervalInMs { get; set; } = 5000;
    }
    
    public class ConfigurationReader : IConfigurationReader
    {
        public string ApplicationName { get; set; }
        AutoResetEvent _autoEvent = null;
        Timer _timer = null;
        private IEnumerable<Configuration> confList;

        private IConfigurationRepository _configurationRepository;

        private int _refreshTimerInterval;

        public CancellationToken CancellationToken { get; set; }

        public ConfigurationReader(string applicationName,string connectionString,int refreshTimerIntervalInMs)
        {
            _configurationRepository = DependencyService.Instance.CurrentResolver.Resolve<IConfigurationRepository>();
            confList = new List<Configuration>();
            _refreshTimerInterval = refreshTimerIntervalInMs;
            applicationName = applicationName;
            Task.Run(() => this.StartTimer(CancellationToken)).Wait();
        }

        public async Task CheckDatas()
        {
            Console.WriteLine("triggered");

            var list = await _configurationRepository.GetAll();
            list = list.Where(v => v.ApplicationName == ApplicationName);
        }

        public async Task StartTimer(CancellationToken cancellationToken)
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
            string type = "string";
            return (T) Activator.CreateInstance<T>();
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