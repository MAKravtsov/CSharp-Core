using Reverse.Algorithms.Base;

namespace Reverse.Algorithms;

internal class Reverse<T> : ReverseAlgorithmBase<T[], T> where T : IComparable
{
    /*
        Суть алгоритма: переворачиваем массив, последовательно проходя по нему до середины,
            меняя первые элементы с последними
        
        Лучший случай: O(n)

        Средний случай: O(n)

        Худший случай: O(n)

        По памяти: О(1)
     */
    
    public Reverse(T[] items) : base(items)
    {
    }
    
    public override string Name => nameof(Reverse<T>);
    public override string CommandLineName => "r";
    public override object Execute()
    {
        var array = new T[Data.Length];
        Data.CopyTo(array, 0);
        Iterations = 0;
        
        var i = 0;
        var j = array.Length - 1;

        for (; i < j; i++, j--)
        {
            Swap(ref array[i], ref array[j]);
            Iterations++;
        }

        return array;
    }

    protected override T[] GetDataStructureFromArray(T[] items)
    {
        return items;
    }
}