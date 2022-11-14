using System.Diagnostics.CodeAnalysis;
using Common.Algorithm;

namespace Sorting.Algorithms.Base;

internal abstract class SortAlgorithmBase<T> : AlgorithmBase<T> where T : IComparable
{
    protected SortAlgorithmBase([NotNull] T[] items) : base(items)
    {
    }

    protected static void Swap(ref T valueA, ref T valueB)
    {
        (valueB, valueA) = (valueA, valueB);
    }
}
