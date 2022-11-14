using Common.Generators.Base;

namespace Common.Generators;

internal class UintArrayGenerator : ArrayGenerator<uint>
{
    public UintArrayGenerator(int count) : base(count)
    {
    }

    public override uint[] Generate()
    {
        var random = new Random();

        var rightBound = Count < MinRightBound 
            ? MinRightBound 
            : Count;

        var array = new uint[Count];

        for (var i = 0; i < Count; i++)
        {
            array[i] = (uint)random.Next(0, rightBound);
        }

        return array;
    }
}
