using System.Diagnostics.CodeAnalysis;

namespace Common.Algorithm;

public abstract class AlgorithmBase<T> : IAlgorithm where T : IComparable
{
    protected T[] Items { get; }

    protected int Iterations;

    public int IterationsCount => Iterations;

    protected AlgorithmBase(
        [NotNull] T[] items)
    {
        Items = new T[items.Length];
        items.CopyTo(Items, 0);
    }

    public abstract string Name { get; }

    public abstract string CommandLineName { get; }

    public abstract object Execute();

    public override string ToString()
    {
        return Name;
    }
}
