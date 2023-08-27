using Common.Generators.Base;

namespace Common.Generators;

internal class StringGenerator : ArrayGenerator<char>
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private static readonly Random Random = new();
    
    public StringGenerator(int count) : base(count)
    {
    }

    public override char[] Generate()
    {
        var array = new char[Count];

        for (var i = 0; i < Count; i++)
        {
            var randomIndex = Random.Next(Chars.Length);
            array[i] = Chars[randomIndex];
        }

        return array;

    }
}