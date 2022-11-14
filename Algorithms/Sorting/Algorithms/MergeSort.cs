using System.Diagnostics.CodeAnalysis;
using Sorting.Algorithms.Base;

namespace Sorting.Algorithms;

internal class MergeSort<T> : SortAlgorithmBase<T> where T : IComparable
{
    /*
        Суть алгоритма:
            Делим массив пополам до тех пор, пока не получим одиночные элементы
            Далее сливаем все эти одиночные элементы попарно, сортируя их в парах -> отсортированные пары
            Далее сливаем пары попарно, сортируя их -> отсортированные четверки
            и тд

            Для слияния используем указатели:
                есть 2 массива
                у каждого указатель на первом элементе
                сравниваем первые элементы этих массивов
                тот, что меньше, добавляем во временный массив
                смещаем указатель вправо у того массива, у которого забрали элемент во временный массив
                когда указатель дошел до конца одного из двух массивов, не раздумывая добавляем все элементы 
                    за указателем другого массива во временный массив
        
        Лучший случай: O(nlogn)

        Средний случай: O(nlogn)

        Худший случай: O(nlogn)

        По памяти: О(n) - временный массив
    */

    public MergeSort([NotNull] T[] items) : base(items)
    {
    }

    public override string Name => nameof(MergeSort<T> );

    public override string CommandLineName => "ms";

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

        var mid = (right + left) / 2;
        Sort(array, left, mid);
        Sort(array, mid + 1, right);
        Merge(array, left, mid, right);

        return array;
    }

    /// <summary>
    /// Слияние
    /// </summary>
    /// <param name="array">общий массив</param>
    /// <param name="left">индекс первого первого элемента первого массива для слияния</param>
    /// <param name="mid">индекс последнего элемента первого массива для слияния
    /// или индекс первого элемента второго массива для слияния</param>
    /// <param name="right">индекс последнего элемента второго массива для слияния</param>
    private void Merge(T[] array, int left, int mid, int right)
    {
        // указатель первого массива
        var leftPoint = left;

        // указатель второго массива
        var rightPoint = mid + 1;

        // временный массив
        var tempArray = new T[right - left + 1];

        // счетчик
        var index = 0;

        // смещаем указатель вправо у того массива, у которого забрали элемент во временный массив
        while(leftPoint <= mid && rightPoint <= right)
        {
            if(array[leftPoint].CompareTo(array[rightPoint]) < 0)
            {
                tempArray[index] = array[leftPoint];
                leftPoint++;
            }
            else
            {
                tempArray[index] = array[rightPoint];
                rightPoint++;
            }

            index++;
            Iterations++;
        }

        // когда указатель дошел до конца одного из двух массивов, не раздумывая добавляем все элементы 
        // за указателем другого массива во временный массив
        for(var i = leftPoint; i <= mid; i++)
        {
            tempArray[index] = array[i];
            index++;
            Iterations++;
        }
        for(var i = rightPoint; i <= right; i++)
        {
            tempArray[index] = array[i];
            index++;
            Iterations++;
        }

        // заполняем результирующий массив из временного
        for(var i = 0; i < tempArray.Length; i++)
        {
            array[left + i] = tempArray[i];
            Iterations++;
        }
    }
}
