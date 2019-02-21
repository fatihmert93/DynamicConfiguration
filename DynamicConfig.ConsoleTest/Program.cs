using System;
using Autofac;
using DynamicConfig.Lib.Abstract;
using DynamicConfig.Lib.IOC;

namespace DynamicConfig.ConsoleTest
{
    class Program
    {
        
        private static IContainer _container;
        
        static void Main(string[] args)
        {
            _container = DependencyService.Instance.CurrentResolver;

            IConfigurationReader _configurationReader = _container.Resolve<IConfigurationReader>();

            string siteName = _configurationReader.GetValue<string>("SiteName");
            
            Console.WriteLine("Sitename: " + siteName);

            bool isBasketEnabled = _configurationReader.GetValue<bool>("IsBasketEnabled");
            
            Console.WriteLine("IsBasketEnabled" + isBasketEnabled);


            Console.ReadKey();
        }
    }
}