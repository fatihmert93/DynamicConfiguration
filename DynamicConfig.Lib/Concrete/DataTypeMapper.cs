using System;
using System.Collections.Generic;

namespace DynamicConfig.Lib.Concrete
{
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