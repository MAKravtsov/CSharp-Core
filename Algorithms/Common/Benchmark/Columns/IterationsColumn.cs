using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Common.Algorithm;

namespace Common.Benchmark.Columns;

internal class IterationsColumn : IColumn
{
    public string Id { get; }
    public string ColumnName { get; }

    public IterationsColumn()
    {
        ColumnName = "Iterations";
        Id = nameof(IterationsColumn) + "." + ColumnName;
    }

    public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;
    public string GetValue(Summary summary, BenchmarkCase benchmarkCase)
    {
        return ((IAlgorithm)benchmarkCase.Parameters[0].Value).IterationsCount.ToString();
    }

    public bool IsAvailable(Summary summary) => true;
    public bool AlwaysShow => true;
    public ColumnCategory Category => ColumnCategory.Custom;
    public int PriorityInCategory => 0;
    public bool IsNumeric => false;
    public UnitType UnitType => UnitType.Dimensionless;
    public string Legend => $"Custom '{ColumnName}' tag column";
    public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style) => GetValue(summary, benchmarkCase);
    public override string ToString() => ColumnName;
}
