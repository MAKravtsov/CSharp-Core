using System.Diagnostics.CodeAnalysis;
using DataStructures.DataStructures.BinaryTree;
using Sorting.Algorithms.Base;

namespace Sorting.Algorithms;

internal class TreeSort<T> : SortAlgorithmBase<T> where T : IComparable
{
    /*
        Суть алгоритма:
            Из входного массива делаем бинарное дерево
            Из бинарного дерева делаем отсортированный массив
            Все подробности реализации смотреть в бинарном дереве

        Лучший случай: O(nlogn)

        Средний случай: O(nlogn)

        Худший случай: O(n^2)

        По памяти: О(n) - результирующий массив
    */
    
    public TreeSort([NotNull] T[] items) : base(items)
    {
    }

    public override string Name => nameof(TreeSort<T>);

    public override string CommandLineName => "ts";

    public override object Execute()
    {
        var length = Items.Length;

        var array = new T[length];
        Items.CopyTo(array, 0);
        Iterations = 0;

        var tree = new BinaryTree<T>(array[0]);

        for (var i = 1; i < length; i++)
        {
            tree.Add(array[i], ref Iterations);
        }

        // сортировка бинарного дерева - это по сути проход массива один раз,
        // если говорить о количестве
        Iterations += length;

        return tree.Inorder();
    }
}
