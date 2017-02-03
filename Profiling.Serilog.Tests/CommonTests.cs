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
    public partial class CommonTests
    {
        private readonly TestSink _testSink;

        public CommonTests()
        {
            _testSink = new TestSink();
            var logger = new LoggerConfiguration().AddMiniProfiler().WriteTo.Sink(_testSink).MinimumLevel.Debug().CreateLogger();

            MiniProfilerLog.SetUpSerilog(logger);
        }

        [Fact]
        public void OneStepExecute_ShouldExactlyOneLogEventWithMessage()
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

        [Fact]
        public void TwoStepExecute_ShouldTwoLogEventsSendToSink()
        {
            StartOneStep();
            StartOneStep();

            _testSink.LogEvents.Should().HaveCount(2);
        }

        [Fact]
        public void LogLevelIsInfo_ProfileByDebug_ZeroLogEventsSendsToSink()
        {
            var logger = new LoggerConfiguration().AddMiniProfiler().WriteTo.Sink(_testSink).MinimumLevel.Information().CreateLogger();

            MiniProfilerLog.SetUpSerilog(logger, LogEventLevel.Debug);
            StartOneStep();

            _testSink.LogEvents.Should().HaveCount(0);
        }


        private static void StartOneStep()
        {
            MiniProfiler.Start();
            MiniProfiler.StepStatic("Step");
            MiniProfiler.Stop();
        }
    }
}