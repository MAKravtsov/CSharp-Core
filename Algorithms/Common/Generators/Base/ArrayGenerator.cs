namespace Common.Generators.Base;

internal abstract class ArrayGenerator<T>
{
    protected readonly int Count;

    protected ArrayGenerator(int count)
    {
        Count = count;
    }

    protected const int MinRightBound = 100;

    public abstract T[] Generate();
}
