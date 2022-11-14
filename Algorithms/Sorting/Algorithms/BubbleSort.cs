using System.Diagnostics.CodeAnalysis;
using Sorting.Algorithms.Base;

namespace Sorting.Algorithms;

internal class BubbleSort<T> : SortAlgorithmBase<T> where T : IComparable
{
    /*
        Суть алгоритма: 
            Попарно сравниваем элементы последовательности,
            если "левый" элемент больше "правого", то меняем местами.
            То есть каждый проход выносим элемент с максимальным значением в конец (как бы пузырек идет наверх)
        
        Лучший случай: O(n^2)

        Средний случай: O(n^2)

        Худший случай: O(n^2)

        По памяти: О(1)

        Примечания:
            1) Сложность совпадает, так как нет нигде break, алгоритм всегда совершает одинаковое количество итераций
    */

    public BubbleSort([NotNull] T[] items) : base(items)
    {
    }

    public override string Name => nameof(BubbleSort<T>);

    public override string CommandLineName => "bs";

    public override object Execute()
    {
        var array = new T[Items.Length];
        Items.CopyTo(array, 0);
        Iterations = 0;

        var count = array.Length;

        for(; count > 1; count--)
        {
            for(var i = 0; i < count - 1; i++)
            {
                Iterations++;

                if(array[i].CompareTo(array[i + 1]) <= 0)
                    continue;

                Swap(ref array[i], ref array[i + 1]);
            }
        }

        return array;
    }
}
