using System.Diagnostics.CodeAnalysis;
using Sorting.Algorithms.Base;

namespace Sorting.Algorithms;

internal class QuickSort<T> : SortAlgorithmBase<T> where T : IComparable
{
    /*
        Суть алгоритма:
            pivot - индекс элемента массива, если этот массив упорядочить так, 
                что слева от этого элемента - все элементы меньше,
                а справа все элементы больше
            находим pivot для последнего элемента массива
            исходный массив разбивается на 2
            аналогично находим pivot для каждого из них
            так рекурсивно сортируем массив

        Лучший случай: O(nlogn)

        Средний случай: O(nlogn)

        Худший случай: O(n^2) - если последний элемент является максимальным при каждом нахождении pivot

        По памяти: О(1)
    */

    public QuickSort([NotNull] T[] items) : base(items)
    {
    }

    public override string Name => nameof(QuickSort<T>);

    public override string CommandLineName => "qs";

    public override object Execute()
    {
        var array = new T[Items.Length];
        Items.CopyTo(array, 0);
        Iterations = 0;

        return Sort(array, 0, array.Length - 1);
    }

    /// <summary>
    /// Сортировка
    /// </summary>
    /// <param name="array">общий массив</param>
    /// <param name="left">индекс первого элемента для сортировки</param>
    /// <param name="right">индекс последнего элемента для сортировки</param>
    /// <returns></returns>
    private T[] Sort(T[] array, int left, int right)
    {
        if(left >= right)
            return array;

        var pivot = GetPivot(array, left, right);
        Sort(array, left, pivot - 1);
        Sort(array, pivot + 1, right);

        return array;
    }

    /// <summary>
    /// Получение pivot
    /// </summary>
    /// <param name="array">общий массив</param>
    /// <param name="left">левый край</param>
    /// <param name="right">правый край</param>
    /// <returns></returns>
    private int GetPivot(T[] array, int left, int right)
    {
        var pivot = left - 1;

        for (var i = left; i <= right; i++)
        {
            Iterations++;

            // если элемент меньше последнего, то все перестановок делать не нужно
            if(array[i].CompareTo(array[right]) >= 0)
                continue;

            // если элемент больше последнего, то ставим этот элемент слево от pivot
            pivot++;
            Swap(ref array[pivot], ref array[i]);
        }

        // меняем местами pivot и последний элемент
        pivot++;
        Swap(ref array[pivot], ref array[right]);
        Iterations++;

        return pivot;
    }
}
