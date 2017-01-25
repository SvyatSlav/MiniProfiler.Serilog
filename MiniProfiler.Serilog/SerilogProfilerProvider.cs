using System;
using Serilog;
using Serilog.Events;
using StackExchange.Profiling;

namespace Profiling.Serilog
{
    public class SerilogProfilerProvider : BaseProfilerProvider
    {
        private readonly ILogger _logger;

        private readonly LogEventLevel _profilerLogLevel;
        private MiniProfiler _profiler;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogProfilerProvider"/> class.
        /// </summary>
        /// <param name="logger">Instance of Serilog logger</param>
        /// <param name="profilerLogLevel">Level which profiler will write in log. Default == Debug</param>
        public SerilogProfilerProvider(ILogger logger, LogEventLevel profilerLogLevel)
        {
            _logger = logger;
            _profilerLogLevel = profilerLogLevel;

            MiniProfiler.Settings.Storage = new SerilogStorage(_logger, _profilerLogLevel);
        }

        public override StackExchange.Profiling.MiniProfiler Start(ProfileLevel level, string sessionName = null)
        {
            return Start(sessionName);
        }

        public override void Stop(bool discardResults)
        {
            if (_profiler != null)
            {
                StopProfiler(_profiler);
                SaveProfiler(_profiler);
            }
        }

        public override StackExchange.Profiling.MiniProfiler GetCurrentProfiler()
        {
            return _profiler;
        }

        public override StackExchange.Profiling.MiniProfiler Start(string sessionName = null)
        {
            _profiler = new MiniProfiler(sessionName ?? AppDomain.CurrentDomain.FriendlyName);
            if (IsLogEnabled(_logger, _profilerLogLevel))
            {
                SetProfilerActive(_profiler);
            }
            else
            {
                StopProfiler(_profiler);
            }


            return _profiler;
        }

        private bool IsLogEnabled(ILogger log, LogEventLevel profilerLogLevel)
        {
            if (log == null)
            {
                return false;
            }
            return log.IsEnabled(profilerLogLevel);
        }

        /// <summary>
        /// Determines whether [is same logger] [the specified logger]. Or ProfilerLogLevel equals
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="profilerLogLevel">The profiler log level.</param>
        /// <returns></returns>
        public bool IsSameLogger(ILogger logger, LogEventLevel profilerLogLevel)
        {
            return _logger == logger && _profilerLogLevel == profilerLogLevel;
        }
    }
}