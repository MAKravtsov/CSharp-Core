namespace Common.Algorithm;

public interface IAlgorithm
{
    int IterationsCount { get; }

    string Name { get; }

    string CommandLineName { get; }

    object Execute();
}
