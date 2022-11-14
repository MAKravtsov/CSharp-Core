using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Common.Benchmark;
using Common.Benchmark.Columns;
using Common.Extensions;
using Common.Generators;
using Common.Generators.Base;

namespace Common.Launch;

public abstract class LauncherBase
{
    private const string DataTypeArg = "-type";

    private const string BenchmarkArg = "-bnm";

    private const string AlgArg = "-alg";

    private const string CountArg = "-cnt";

    private const char Separator = '=';

    protected abstract void RegisterAlgs<T>(T[] array) where T : IComparable;

    protected virtual void ShowAlgResult<T>(object result) { }

    public void Launch(string[] args)
    {
        if(!args.Any(y => y.StartsWith(DataTypeArg)))
        {
            throw new Exception("Has not data type");
        }
                
        var dataType = args.First(y => y.StartsWith(DataTypeArg))
            .Split(Separator)
            .Last();

        string? countStr = null;

        if(args.Any(y => y.StartsWith(CountArg)))
        {
            countStr = args.First(y => y.StartsWith(CountArg))
                .Split(Separator)
                .Last();
        }
        else
        {
            Console.Write("Array count: ");
            countStr = Console.ReadLine();
        }

        if (!int.TryParse(countStr, out var count))
            throw new Exception("Array count has wrong format");

        switch(dataType)
        {
            case "ui":
                ArrayGenerator<uint> uintGenerator = new UintArrayGenerator(count);
                Launch(uintGenerator, args);

                break;

            case "i":
                ArrayGenerator<int> intGenerator = new IntArrayGenerator(count);
                Launch(intGenerator, args);

                break;
            
            default:
                throw new Exception("Data type not correct");
        }
    }

    private void Launch<T>(
        ArrayGenerator<T> generator,
        string[] args) where T : IComparable
    {
        var array = generator.Generate();
        Console.WriteLine(array.ToJsonString());

        RegisterAlgs(array);

        if(args.Any(y => y.StartsWith(BenchmarkArg)))
        {
            var column = new IterationsColumn();
            var config = new AlgorithmConfig().AddColumn(column);

            BenchmarkRunner.Run<Runner>(config);
        }
        else if(args.Any(y => y.StartsWith(AlgArg)))
        {
            var algName = args.First(y => y.StartsWith(AlgArg))
                .Split(Separator)
                .Last();

            var alg = Locator.Locator.Instance.Get(y => y.CommandLineName == algName);

            if(alg == null)
                throw new Exception("Algorithm type not correct");

            var items = alg.Execute();

            ShowAlgResult<T>(items);

            Console.WriteLine($"Iterstions: {alg.IterationsCount}");
        }
    }
}
