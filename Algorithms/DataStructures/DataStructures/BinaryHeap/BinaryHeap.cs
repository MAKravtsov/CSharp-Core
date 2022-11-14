namespace DataStructures.DataStructures.BinaryHeap;

/// <summary>
/// Бинарная куча
/// </summary>
/// <typeparam name="T"></typeparam>
public class BinaryHeap<T> where T : IComparable
{
    /*
        Дерево, у каждого листика которого <= 1 родителей и <= 2 детей
        У каждого листика оба ребенка обязательно ниже по значению, чем родитель
    */

    private readonly List<T> _items;

    private int HeapSize => _items.Count;

    public BinaryHeap(T value)
    {
        _items = new List<T> { value };
    }

    public void Add(T value, ref int iterations)
    {
        _items.Add(value);
        var index = HeapSize - 1;

        if(index <= 0)
            return;

        var parentIndex = GetParentIndex(index);

        while(index > 0 && _items[index].CompareTo(_items[parentIndex]) > 0)
        {
            Swap(index, parentIndex);

            index = parentIndex;
            parentIndex = GetParentIndex(index);

            iterations++;
        }
    }

    /// <summary>
    /// Метод работает только когда мы поменяли какой-то один элемент в куче,
    /// при этом вместо него поставили элемент ниже того, который поменяли
    /// </summary>
    /// <param name="index">Индекс меняемого элемента</param>
    public void Heapify(int index, ref int iterations)
    {
        var largestIndex = index;
        var leftChildIndex = GetLeftChildIndex(index);
        var rightChildIndex = GetRightChildIndex(index);

        while(true)
        {
            iterations++;

            if(leftChildIndex < HeapSize && _items[leftChildIndex].CompareTo(_items[largestIndex]) > 0)
            {
                largestIndex = leftChildIndex;
            }

            if(rightChildIndex < HeapSize && _items[rightChildIndex].CompareTo(_items[largestIndex]) > 0)
            {
                largestIndex = rightChildIndex;
            }

            if(largestIndex == index)
                break;

            Swap(index, largestIndex);
            index = largestIndex;
            leftChildIndex = GetLeftChildIndex(index);
            rightChildIndex = GetRightChildIndex(index);
        }
    }

    public T ExtractMax()
    {
        var result = _items[0];
        _items[0] = _items[HeapSize - 1];
        _items.RemoveAt(0);
        return result;
    }

    private static int GetParentIndex(int index)
    {
        return (index - 1) / 2;
    }

    private static int GetLeftChildIndex(int index)
    {
        return 2 * index + 1;
    }

    private static int GetRightChildIndex(int index)
    {
        return 2 * index + 2;
    }

    /// <summary>
    /// Обязательна реализация именно такая!!! Без (a, b) = (b, a)
    /// </summary>
    /// <param name="indexA"></param>
    /// <param name="indexB"></param>
    private void Swap(int indexA, int indexB)
    {
        var temp = _items[indexA];
        _items[indexA] = _items[indexB];
        _items[indexB] = temp;
    }
}
