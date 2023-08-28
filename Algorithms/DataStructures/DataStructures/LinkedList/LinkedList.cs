using System.Text;

namespace DataStructures.DataStructures.LinkedList;

public class LinkedList<T>
{
    public Node<T>? Head { get; }

    public LinkedList(Node<T> head)
    {
        Head = head;
    }
    
    public LinkedList(IEnumerable<T> collection)
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
                current.Next.Prev = current;
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
            if (current.Prev != null)
            {
                result.Append($"{current.Prev.Value} <- ");
            }
            else
            {
                result.Append($"       ");
            }

            result.Append($"{current.Value}");

            if (current.Next != null)
            {
                result.Append($" -> {current.Next.Value}");
            }
            
            result.AppendLine();

            current = current.Next;
        }
        
        return result.ToString();
    }
}