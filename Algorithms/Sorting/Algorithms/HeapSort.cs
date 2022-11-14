using System.Diagnostics.CodeAnalysis;
using DataStructures.DataStructures.BinaryHeap;
using Sorting.Algorithms.Base;

namespace Sorting.Algorithms;

internal class HeapSort<T> : SortAlgorithmBase<T> where T : IComparable
{
    /*
        Суть алгоритма:
            Из входного массива делаем бинарную кучу
            Из бинарной кучи вытаскиваем первый (максимальный) элемент, перестраиваем ее и снова вытаскиваем
            до тех пор, пока куча не закончится

        Лучший случай: O(nlogn)

        Средний случай: O(nlogn)

        Худший случай: O(nlogn)

        По памяти: О(1)
    */

    public HeapSort([NotNull] T[] items) : base(items)
    {
    }

    public override string Name => nameof(HeapSort<T>);

    public override string CommandLineName => "hs";

    public override object Execute()
    {
        var length = Items.Length;

        var array = new T[length];
        Items.CopyTo(array, 0);
        Iterations = 0;

        var heap = new BinaryHeap<T>(array[0]);

        for (var i = 1; i < length; i++)
        {
            heap.Add(array[i], ref Iterations);
        }

        for(var i = 0; i < length; i++)
        {
            array[length - 1 - i] = heap.ExtractMax();
            Iterations++;
            heap.Heapify(0, ref Iterations);
        }

        return array;
    }
}
