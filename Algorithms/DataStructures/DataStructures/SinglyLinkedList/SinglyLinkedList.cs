using System.Text;

namespace DataStructures.DataStructures.SinglyLinkedList;

public class SinglyLinkedList<T>
{
    public Node<T>? Head { get; }

    public SinglyLinkedList(Node<T> head)
    {
        Head = head;
    }
    
    public SinglyLinkedList(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        Node<T>? current = null;
        foreach (var item in collection)
        {
            var node = new Node<T>
            {
                Value = item,
            };
            
            if (current == null)
            {
                Head = node;
                current = Head;
            }
            else
            {
                current.Next = node;
                current = current.Next;
            }
        }
    }

    public string ToJsonString()
    {
        var result = new StringBuilder();
        
        var current = Head;

        while (current != null)
        {
            result.Append($"{current.Value}");

            if (current.Next != null)
            {
                result.Append($" -> {current.Next.Value}");
                result.AppendLine();
            }

            current = current.Next;
        }
        
        return result.ToString();
    }
}