using DataStructures.DataStructures.SinglyLinkedList;
using Reverse.Algorithms.Base;

namespace Reverse.Algorithms;

internal class SinglyLinkedListReverse<T> : ReverseAlgorithmBase<SinglyLinkedList<T>, T> where T : IComparable
{
    public SinglyLinkedListReverse(T[] items) : base(items)
    {
    }

    public override string Name => nameof(SinglyLinkedListReverse<T>);
    public override string CommandLineName => "sllr";
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

        while (curr != null)
        {
            temp = curr.Next;
            curr.Next = prev;
            prev = curr;
            curr = temp;
        }

        head = prev;

        return new SinglyLinkedList<T>(head);
    }

    protected override SinglyLinkedList<T> GetDataStructureFromArray(T[] items)
    {
        return new SinglyLinkedList<T>(items);
    }
}