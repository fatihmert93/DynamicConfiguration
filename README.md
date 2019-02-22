# DynamicConfiguration

Supports .Net Standard 2.0.

## Before use the library you need to execute this docker command on your project file
```docker
docker-compose up
```

## Mongodb insert data example
```csharp
db.Configurations.insert({"Name": "SiteName", "Type":"string", "IsActive": true,"ApplicationName": "ServiceA","Value":"boyner.com.tr"})
```
## Data Types

|      C# Type. |ConfigType|
|-------------- |---------:|
|   		int | integer  |
|        string | string   |
|      DateTime | datetime |
|          bool | boolean  |
|         float | float    |
|       decimal | decimal  |
|      TimeSpan | timespan |
|          Guid | guid     |

## API

```csharp
private static IContainer _container;
_container = DependencyService.Instance.CurrentResolver;

IConfigurationReader _configurationReader = _container.Resolve<IConfigurationReader>();
```
Config settings for Application service name, connection string and refreshtime
If you leave the ConnectionString field blank it use 'localhost' as default

```csharp
ConfigSettings.ApplicationName = "ServiceA";
ConfigSettings.ConnectionString = "mongodb://localhost:27017";
ConfigSettings.RefreshTimerIntervalInMs = 5000;
```

After theese configuration if you add to you mongo db some config data you can call you data value

```csharp
string siteName = _configurationReader.GetValue<string>("SiteName");
            
Console.WriteLine("Sitename: " + siteName);

bool isBasketEnabled = _configurationReader.GetValue<bool>("IsBasketEnabled");
            
Console.WriteLine("IsBasketEnabled" + isBasketEnabled);
```

## Where can I get it?

You can install [DynamicConfiguration](https://www.nuget.org/packages/DynamicConfiguration/) from the package manager console:

```
PM> Install-Package DynamicConfiguration
```

***

Developed with :heart: at [Fatih Mert](https://fatihmert.net).


