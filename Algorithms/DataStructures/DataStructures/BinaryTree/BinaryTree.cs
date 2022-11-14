namespace DataStructures.DataStructures.BinaryTree;

/// <summary>
/// Бинарное дерево
/// </summary>
/// <typeparam name="T"></typeparam>
public class BinaryTree<T> where T : IComparable
{
    /*
        Дерево, у каждого листика которого <= 1 родителей и <= 2 детей
        У каждого листика ребенок слева обязательно меньше, а ребенок справа больше или равен
    */

    private readonly T _value;

    private readonly BinaryTree<T>? _parent;

    private BinaryTree<T>? _left;

    private BinaryTree<T>? _right;

    public BinaryTree(T value)
    {
        _value = value;
    }

    private BinaryTree(T value, BinaryTree<T> parent) : this(value)
    {
        _parent = parent;
    }

    public void Add(T value, ref int iterations)
    {
        if(value.CompareTo(_value) < 0)
        {
            if(_left == null)
            {
                _left = new BinaryTree<T>(value, this);
            }
            else
            {
                _left.Add(value, ref iterations);
            }
        }
        else
        {
            if(_right == null)
            {
                _right = new BinaryTree<T>(value, this);
            }
            else
            {
                _right.Add(value, ref iterations);
            }
        }

        iterations++;
    }

    public T[] Inorder()
    {
        var result = new List<T>();

        if(_left != null)
        {
            result.AddRange(_left.Inorder());
        }

        result.Add(_value);

        if(_right != null)
        {
            result.AddRange(_right.Inorder());
        }

        return result.ToArray();
    }
}
