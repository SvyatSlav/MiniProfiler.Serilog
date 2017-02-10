# MiniProfiler.Serilog

MiniProfiler Extension allows save results into <a href="https://serilog.net/" target="_blank">Serilog</a>

Inspired by <a href="https://github.com/SvyatSlav/MiniProfiler.Log4Net" target="_blank">MiniProfiler.Log4Net</a> and  Dave Glick's [post](https://daveaglick.com/posts/easy-performance-and-query-logging-in-aspnet-with-serilog-and-miniprofiler)

## Install from Nuget

```
 Install-Package MiniProfiler.Serilog 
```

## Usage
Setup Serilog:

```cSharp
Log.Logger = new LoggerConfiguration().AddMiniProfiler() ...;
```

And than setup Miniprofiler with serilog's Logger

```cSharp
 MiniProfilerLog.SetUpSerilog();
 ```
 
And this all what you need!

### Result examples
In Seq:

[![seq.png](https://s27.postimg.org/afz0v4lg3/screen.png)](https://postimg.org/image/hj6waqqvj/)

Use MiniProfiler as usual (see [MiniProfiler site](http://miniprofiler.com/)) 