using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using StackExchange.Profiling;
using StackExchange.Profiling.Storage;

namespace Profiling.Serilog
{
    public class SerilogStorage : IStorage
    {
        private readonly ILogger _logger;
        private readonly LogEventLevel _profilingLevel;

        public SerilogStorage(ILogger logger, LogEventLevel profilingLevel)
        {
            _logger = logger;
            _profilingLevel = profilingLevel;
        }

        public IEnumerable<Guid> List(int maxResults, DateTime? start = null, DateTime? finish = null, ListResultsOrder orderBy = ListResultsOrder.Descending)
        {
            //TODO NotImplementedException();
            return null;
        }

        public void Save(MiniProfiler profiler)
        {
            if (_logger == null)
            {
                return;
            }

            switch (_profilingLevel)
            {
                //TODO DRY!
                case LogEventLevel.Verbose:
                    if (_logger.IsEnabled(_profilingLevel))
                    {
                        GetLogWithContext(_logger).Verbose("Completed step in {Timing} ms", MiniProfiler.Current.Root.DurationMilliseconds);
                    }
                    break;
                case LogEventLevel.Debug:
                    if (_logger.IsEnabled(_profilingLevel))
                    {
                        GetLogWithContext(_logger).Debug("Completed step in {Timing} ms", MiniProfiler.Current.Root.DurationMilliseconds);
                    }
                    break;
                case LogEventLevel.Information:
                    if (_logger.IsEnabled(_profilingLevel))
                    {
                        GetLogWithContext(_logger).Information("Completed step in {Timing} ms", MiniProfiler.Current.Root.DurationMilliseconds);
                    }
                    break;
                case LogEventLevel.Warning:
                    if (_logger.IsEnabled(_profilingLevel))
                    {
                        GetLogWithContext(_logger).Warning("Completed step in {Timing} ms", MiniProfiler.Current.Root.DurationMilliseconds);
                    }
                    break;
                case LogEventLevel.Error:
                    if (_logger.IsEnabled(_profilingLevel))
                    {
                        GetLogWithContext(_logger).Error("Completed step in {Timing} ms", MiniProfiler.Current.Root.DurationMilliseconds);
                    }
                    break;
                case LogEventLevel.Fatal:
                    if (_logger.IsEnabled(_profilingLevel))
                    {
                        GetLogWithContext(_logger).Fatal("Completed step in {Timing} ms", MiniProfiler.Current.Root.DurationMilliseconds);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ILogger GetLogWithContext(ILogger logger)
        {
            return logger
                .ForContext("MiniProfiler", JsonConvert.DeserializeObject<dynamic>(MiniProfiler.ToJson()), true);
        }


        public MiniProfiler Load(Guid id)
        {
            //TODO NotImplementedException();
            return null;
        }

        public void SetUnviewed(string user, Guid id)
        {
            //TODO NotImplementedException();
        }

        public void SetViewed(string user, Guid id)
        {
            //TODO NotImplementedException();
        }

        public List<Guid> GetUnviewedIds(string user)
        {
            //TODO NotImplementedException();
            return null;
        }
    }
}