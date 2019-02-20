using System;
using Autofac;

namespace DynamicConfig.Lib.IOC
{
    public class DependencyService
    {
        private DependencyService()
        {

        }
        
        static readonly Lazy<DependencyService> _resolver = new Lazy<DependencyService>(() => new DependencyService());

        public static DependencyService Instance = _resolver.Value;

        private IContainer _currentResolver;

        public virtual IContainer CurrentResolver => _currentResolver ?? (_currentResolver = BuildContainer());

        public IContainer BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            IContainer container = builder.Bind();
            return container;
        }
        
    }
}