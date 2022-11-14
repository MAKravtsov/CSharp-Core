using Common.Generators.Base;

namespace Common.Generators;

internal class IntArrayGenerator : ArrayGenerator<int>
{
    public IntArrayGenerator(int count) : base(count)
    {
    }

    public override int[] Generate()
    {
        var random = new Random();

        var rightBound = Count < MinRightBound 
            ? MinRightBound 
            : Count;

        var array = new int[Count];

        for (var i = 0; i < Count; i++)
        {
            array[i] = random.Next(-rightBound, rightBound);
        }

        return array;
    }
}
