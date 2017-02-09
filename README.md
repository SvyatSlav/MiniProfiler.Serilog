# MiniProfiler.Serilog

MiniProfiler Extension allows save results into <a href="https://serilog.net/" target="_blank">Serilog</a>

Inspired by <a href="https://github.com/SvyatSlav/MiniProfiler.Log4Net" target="_blank">MiniProfiler.Log4Net</a> and  Dave Glick [post](https://daveaglick.com/posts/easy-performance-and-query-logging-in-aspnet-with-serilog-and-miniprofiler)

## Install from Nuget

```
 Install-Package MiniProfiler.Serilog 
```

## Usage
Setup Serilog:

```cSharp
new LoggerConfiguration().AddMiniProfiler() ...;
```

And than setup Miniprofiler with serilog's Logger

```cSharp
 MiniProfilerLog.SetUpSerilog(Log.Logger);
 ```
 
And this all what you need!

Use Profiler as usual (see [MiniProfiler site](http://miniprofiler.com/)) 