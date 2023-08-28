using DataStructures.DataStructures.LinkedList;
using Reverse.Algorithms.Base;

namespace Reverse.Algorithms;

public class LinkedListReverse<T> :
    ReverseAlgorithmBase<DataStructures.DataStructures.LinkedList.LinkedList<T>, T> where T : IComparable
{
    public LinkedListReverse(T[] items) : base(items)
    {
    }

    public override string Name => nameof(LinkedListReverse<T>);
    public override string CommandLineName => "llr";
    public override object Execute()
    {
        var head = Data.Head;

        if (head == null)
        {
            throw new Exception("Односвязанный список не заполнен.");
        }
        
        Node<T>? temp;
        var prev = head;
        var curr = head.Next;

        prev.Next = null;
        prev.Prev = curr;

        while (curr != null)
        {
            temp = curr.Next;
            curr.Next = prev;
            curr.Prev = temp;
            prev = curr;
            curr = temp;
        }

        head = prev;

        return new DataStructures.DataStructures.LinkedList.LinkedList<T>(head);
    }

    protected override DataStructures.DataStructures.LinkedList.LinkedList<T> GetDataStructureFromArray(T[] items)
    {
        return new DataStructures.DataStructures.LinkedList.LinkedList<T>(items);
    }
}