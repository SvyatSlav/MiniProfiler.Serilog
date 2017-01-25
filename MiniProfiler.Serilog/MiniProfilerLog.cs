using Destructurama;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using StackExchange.Profiling;


namespace Profiling.Serilog
{
    /// <summary>
    /// Extensions MiniProfiler for log into Log4net
    /// </summary>
    public static class MiniProfilerLog
    {
        /// <summary>
        /// Setup profiler with Serilog-logger and log level
        /// </summary>
        /// <param name="logger">Instance of Serilog logger</param>
        /// <param name="logLevel">Level which identified as Profiler message writable. Default == Debug</param>
        public static void SetUpSerilog(ILogger logger = null, LogEventLevel logLevel = LogEventLevel.Debug)
        {
            var currentProvider = MiniProfiler.Settings.ProfilerProvider as SerilogProfilerProvider;

            if (logger == null || currentProvider == null || !currentProvider.IsSameLogger(logger, logLevel))
            {
                var provider = new SerilogProfilerProvider(logger ?? Log.Logger, logLevel); //TODO Log.logger.Destructer?
                MiniProfiler.Settings.ProfilerProvider = provider;
            }
        }

        public static void SetUpSerilog(this MiniProfiler profiler, ILogger logger = null, LogEventLevel logLevel = LogEventLevel.Debug)
        {
            SetUpSerilog(logger, logLevel);
        }

        public static LoggerConfiguration AddMiniProfiler(this LoggerConfiguration configuration)
        {
            return configuration.Destructure.JsonNetTypes();
        }
    }
}