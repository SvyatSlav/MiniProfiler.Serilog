using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace Profiling.Serilog.Tests
{
    public class TestSink : ILogEventSink
    {
        public List<LogEvent> LogEvents { get; set; } = new List<LogEvent>();

        public void Emit(LogEvent logEvent)
        {
            LogEvents.Add(logEvent);
        }
    }
}