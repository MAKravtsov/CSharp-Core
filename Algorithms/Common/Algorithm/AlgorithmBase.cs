namespace Common.Algorithm;

public abstract class AlgorithmBase<T> : IAlgorithm where T : IComparable
{
    protected int Iterations;

    public int IterationsCount => Iterations;

    public abstract string Name { get; }

    public abstract string CommandLineName { get; }

    public abstract object Execute();

    protected static void Swap(ref T valueA, ref T valueB)
    {
        (valueB, valueA) = (valueA, valueB);
    }
    
    public override string ToString()
    {
        return Name;
    }
}
