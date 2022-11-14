using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace Common.Benchmark;

internal class AlgorithmConfig : DebugInProcessConfig
{
    public override IEnumerable<Job> GetJobs()
    {
        var job = base.GetJobs().First()
            .WithLaunchCount(1)
            .WithWarmupCount(1)
            .WithIterationCount(1);

        return new[] { job };
    }
}
