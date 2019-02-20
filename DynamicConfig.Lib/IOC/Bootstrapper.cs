using Autofac;

namespace DynamicConfig.Lib.IOC
{
    public class Bootstrapper
    {
        public IContainer Container { get; set; }
        
        public virtual void DependencyResolving(IContainer instanceCurrentResolver)
        {
            
        }
    }
}