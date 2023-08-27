using System.Diagnostics.CodeAnalysis;
using Common.Algorithm;

namespace Sorting.Algorithms.Base;

internal abstract class SortAlgorithmBase<T> : AlgorithmBase<T> where T : IComparable
{
    protected T[] Items { get; }
    
    protected SortAlgorithmBase([NotNull] T[] items)
    {
        Items = new T[items.Length];
        items.CopyTo(Items, 0);
    }
}
