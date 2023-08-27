using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Common.Algorithm;

namespace Common.Benchmark;

[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class Runner
{
    [Benchmark]
    [ArgumentsSource(nameof(Data))]
    public void Exec(IAlgorithm algorithm)
    {
        algorithm.Execute();
    }

    public static IEnumerable<object> Data()
    {
        var algorithms = Locator.Locator.Instance.GetAll();

        foreach(var alg in algorithms)
        {
            yield return alg;
        }
    }
}
