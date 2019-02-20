using System;
using Autofac;
using DynamicConfig.Lib.Abstract;
using DynamicConfig.Lib.Concrete;
using DynamicConfig.Lib.IOC;
using Xunit;

namespace DynamicConfig.Test
{
    public class ConfigurationReaderTest
    {
        private IContainer _container;
        
        [Fact]
        public void Is_Type_Mapper_Work()
        {
            _container = DependencyService.Instance.CurrentResolver;

            IConfigurationReader _reader = _container.Resolve<IConfigurationReader>();

            Type stringType = _reader.TypeMap("string");
            Type floatType = _reader.TypeMap("float");
            
            Assert.Equal(typeof(string),stringType);
            Assert.Equal(typeof(float),floatType);
            //Assert.IsType<float>(floatType);

        }

        [Fact]
        public void IsListWorking()
        {
            _container = DependencyService.Instance.CurrentResolver;

            IConfigurationReader _reader = _container.Resolve<IConfigurationReader>();

            _reader.CheckDatas();
        }
    }
}