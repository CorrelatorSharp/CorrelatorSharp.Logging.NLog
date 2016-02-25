## What is this?

CorrelatorSharp.Logging.NLog is an adapter for [CorrelatorSharp.Logging](https://github.com/CorrelatorSharp/CorrelatorSharp.Logging) which enables support for [CorrelatorSharp](http://correlatorsharp.github.io) when using Nlog as your logging framework is an add-on for RestSharp that enables support for . 

## Get it

|   Where    |    What   |
|-------------|-------------|
| NuGet       | [CorrelatorSharp.Logging.NLog](https://www.nuget.org/packages/CorrelatorSharp.Logging.NLog/)
| Latest Build (master)      |   [![Build status](https://ci.appveyor.com/api/projects/status/t4cafqnuo5a22n7i/branch/master?svg=true)](https://ci.appveyor.com/project/CorrelatorSharp/correlatorsharp-logging-nlog/branch/master)  |


## Using it

**Sample:** https://github.com/CorrelatorSharp/CorrelatorSharp.Logging.NLog/tree/master/CorrelatorSharp.Logging.NLog.Sample

To use the NLog component:

1. Install the CorrelatorSharp NLog package
2. Add `<add assembly="CorrelatorSharp.Logging.NLog"/>` to `<extensions>` in the NLog config
3. Adjust your target layout to use the layout renderers below if you want to log corerlation information
4. Replace `using NLog` with `using CorrelatorSharp.Logging`
5. Enable NLog in CorrelationSharp.Logging at start-up of your application/service: `LoggingConfiguration.Current.UseNLog();`

Layout renderers:

 *  `${cs-activity-id}`: current activity id
 *  `${cs-activity-parentid}`: current activity id
 *  `${cs-activity-name}`: current activity name


## Example

**Sample:** https://github.com/CorrelatorSharp/CorrelatorSharp.Logging.NLog/tree/master/CorrelatorSharp.Logging.NLog.Sample


```csharp
using CorrelatorSharp;
using CorrelatorSharp.Logging.NLog;

class Program
{
    private static ILogger _logger;

    static void Main(string[] args)
    {
        LoggingConfiguration.Current.UseNLog();

        _logger = LogManager.GetLogger("NLogSample");

        using (var scope = new ActivityScope("Operation")) {
            _logger.LogTrace("something happened");
        }
    }
}
```

Nlog configuration:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      xsi:schemaLocation="http://www.nlog-project.org/schemas/NLog.xsd NLog.xsd"
      autoReload="true"
      throwExceptions="false"
      internalLogLevel="Off" internalLogFile="c:\temp\nlog-internal.log" >


  <extensions>
    <add assembly="CorrelatorSharp.Logging.NLog"/>
  </extensions>

  <targets>
    <target name="file" xsi:type="File" fileName="log.txt" 
            layout="[activity: ${cs-activity-id}] [parent: ${cs-activity-parentid}] [activity name: ${cs-activity-name}] ${message}"/>
  </targets>

  <rules>
    <logger name="*" minlevel="Trace" writeTo="file" />
  </rules>
</nlog>
```

Sample output:

```
[activity: 12baa714-384b-4236-a47c-7acfd9464500] [parent: ] [activity name: Main Operation] preparing to do work
[activity: 12baa714-384b-4236-a47c-7acfd9464500] [parent: ] [activity name: Main Operation] doing work
[activity: 09d4359a-d191-4bd7-bd68-adafcb23133a] [parent: 12baa714-384b-4236-a47c-7acfd9464500] [activity name: Nested Operation 1] done processing
[activity: 9ddb53d6-3ea0-44da-a7b8-96d911ecf6eb] [parent: 12baa714-384b-4236-a47c-7acfd9464500] [activity name: Nested Operation 2] done processing

```
