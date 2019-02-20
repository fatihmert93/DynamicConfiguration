using System;
using System.Threading.Tasks;

namespace DynamicConfig.Lib.Abstract
{
    public interface IConfigurationReader
    {
        T GetValue<T>(string key);
        Type TypeMap(string type);
        Task CheckDatas();
    }
    
    
}