namespace Leetcode.Tasks;

/*
 * https://leetcode.com/problems/letter-combinations-of-a-phone-number/
 */

public class PhoneNumberLetterCombinations
{
    private readonly Dictionary<char, char[]> _digitsLettersDict = new()
    {
        { '2', new[] { 'a', 'b', 'c' } },
        { '3', new[] { 'd', 'e', 'f' } },
        { '4', new[] { 'g', 'h', 'i' } },
        { '5', new[] { 'j', 'k', 'l' } },
        { '6', new[] { 'm', 'n', 'o' } },
        { '7', new[] { 'p', 'q', 'r', 's' } },
        { '8', new[] { 't', 'u', 'v' } },
        { '9', new[] { 'w', 'x', 'y', 'z' } }
    };

    private const int MaxElementCnt = 4;

    public IList<string> LetterCombinations(string digits) {
        var digitsArr = digits.ToCharArray();

        if(digitsArr.Length > MaxElementCnt)
        {
            throw new Exception($"More than {MaxElementCnt} elements");
        }

        IList<string> result = new List<string>();
        foreach(var digit in digitsArr)
        {
            if(!_digitsLettersDict.ContainsKey(digit))
            {
                throw new Exception("Not correct digit in input string");
            }

            result = Union(result, _digitsLettersDict[digit]);
        }

        return result;
    }

    private static IList<string> Union(IList<string> first, char[] second)
    {
        if(!first.Any())
        {
            return second.Select(y => y.ToString()).ToList();
        }

        var result = new List<string>();
        foreach(var f in first)
        {
            foreach(var s in second)
            {
                result.Add(f + s);
            }
        }
        return result;
    }
}