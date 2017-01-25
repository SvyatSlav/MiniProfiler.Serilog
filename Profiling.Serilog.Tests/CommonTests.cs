using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using StackExchange.Profiling;
using Xunit;

namespace Profiling.Serilog.Tests
{
    public class CommonTests
    {
        private readonly TestSink _testSink;

        private class TestSink : ILogEventSink
        {
            public List<LogEvent> LogEvents { get; set; } = new List<LogEvent>();

            public void Emit(LogEvent logEvent)
            {
                LogEvents.Add(logEvent);
            }
        }

        public CommonTests()
        {
            _testSink = new TestSink();
            var logger = new LoggerConfiguration().AddMiniProfiler().WriteTo.Sink(_testSink).MinimumLevel.Debug().CreateLogger();

            MiniProfilerLog.SetUpSerilog(logger);
        }

        [Fact]
        public void OneStepExecute_ShouldExactlyOneEventWithMessage()
        {
            var mp = MiniProfiler.Start();

            mp.Step("SimpleStep");

            MiniProfiler.Stop();

            _testSink.LogEvents.Should().HaveCount(1);

            var logEvent = _testSink.LogEvents.First();

            logEvent.RenderMessage().Should().Contain("Completed step");
            logEvent.Level.ShouldBeEquivalentTo(LogEventLevel.Debug);
            logEvent.Properties.Should().Contain(p => p.Key == "MiniProfiler");

            var properties = logEvent.Properties["MiniProfiler"];
            properties.ToString().Should().Contain("SimpleStep");
        }
    }
}