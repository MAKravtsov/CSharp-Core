using System.Diagnostics.CodeAnalysis;
using Sorting.Algorithms.Base;

namespace Sorting.Algorithms;

internal class ShakerSort<T> : SortAlgorithmBase<T> where T : IComparable
{
    /*
        Суть алгоритма:
            Попарно сравниваем элементы последовательности,
            если "левый" элемент больше "правого", то меняем местами, как в сортиовке пузырьком.
            Когда вынесли элемент с максимальным значением наверх идем ОБРАТНО,
            вынося элемент с минимальным значением вниз.

            Алгоритм останавливается, если за один проход (неважно вправо или влево)
            не было ни одной смены.

        Лучший случай: O(n)

        Средний случай: O(n^2)

        Худший случай: O(n^2)

        По памяти: О(1)
    */

    public ShakerSort([NotNull] T[] items) : base(items)
    {
    }

    public override string Name => nameof(ShakerSort<T>);

    public override string CommandLineName => "shs";

    public override object Execute()
    {
        var array = new T[Items.Length];
        Items.CopyTo(array, 0);
        Iterations = 0;

        var cnt = array.Length;

        for(var i = 0; i < cnt/2; i++)
        {
            var swapped = false;

            // --->
            for(var j = i; j < cnt - i - 1; j++)
            {
                if(array[j].CompareTo(array[j + 1]) <= 0)
                    continue;

                Swap(ref array[j], ref array[j + 1]);

                swapped = true;

                Iterations++;
            }

            if(!swapped)
                break;

            // <---
            for(var j = cnt - i - 2; j > i; j--)
            {
                if(array[j - 1].CompareTo(array[j]) <= 0)
                    continue;

                Swap(ref array[j - 1], ref array[j]);

                swapped = true;

                Iterations++;
            }

            if(!swapped)
                break;
        }

        return array;
    }
}
