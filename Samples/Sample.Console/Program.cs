using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profiling.Serilog;
using Serilog;
using Serilog.Events;
using StackExchange.Profiling;

namespace Sample.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .AddMiniProfiler()
                .MinimumLevel.Verbose()
                .WriteTo.Seq("http://localhost:5341/")
                .WriteTo.RollingFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs\\log-{Date}.txt"))
                .CreateLogger();

            MiniProfilerLog.SetUpSerilog();

            MiniProfiler.Start();
            MiniProfiler.StepStatic("SomeStep");
            MiniProfiler.Stop();

            Log.Logger.Debug("Some");


            Log.CloseAndFlush();
        }
    }
}