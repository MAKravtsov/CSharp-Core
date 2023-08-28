using System.Diagnostics.CodeAnalysis;
using Common.Algorithm;

namespace Reverse.Algorithms.Base;

public abstract class ReverseAlgorithmBase<TDataStructure, T> : AlgorithmBase<T>
    where T : IComparable
    where TDataStructure : class
{
    protected ReverseAlgorithmBase([NotNull] T[] items)
    {
        Data = GetDataStructureFromArray(items);
    }

    protected TDataStructure Data { get; }

    protected abstract TDataStructure GetDataStructureFromArray(T[] items);
}