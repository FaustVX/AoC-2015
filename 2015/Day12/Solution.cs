#nullable enable
namespace AdventOfCode.Y2015.Day12;

[ProblemName("JSAbacusFramework.io")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    => Parse(input, false);

    private static int Parse(ReadOnlySpan<char> input, bool ignoreRed)
    {
        var sum = 0;
        Parse(input, ref sum, ignoreRed, false, out _);
        return sum;
    }

    private static int Parse(ReadOnlySpan<char> input, ref int sum, bool ignoreRed, bool isInObject, out bool isIgnored)
    => input[0] switch
    {
        '[' => ParseArray(input[1..], ref sum, ignoreRed, false, out isIgnored) + 1,
        '{' => ParseObject(input[1..], ref sum, ignoreRed, false, out isIgnored) + 1,
        '"' => ParseString(input[1..], ref sum, ignoreRed, isInObject, out isIgnored) + 1,
        '-' or (>= '0' and <= '9') => ParseInt(input[0..], ref sum, ignoreRed, false, out isIgnored),
        _ => throw new UnreachableException()
    };

    private static int ParseArray(ReadOnlySpan<char> input, ref int sum, bool ignoreRed, bool isInObject, out bool isIgnored)
    {
        isIgnored = false;
        var length = 0;
        while (input[length] != ']')
        {
            length += Parse(input[length..], ref sum, ignoreRed, false, out isIgnored);
            if (input[length] is ',')
                length++;
        }
        return length + 1;
    }

    private static int ParseObject(ReadOnlySpan<char> input, ref int sum, bool ignoreRed, bool isInObject, out bool isIgnored)
    {
        var originalSum = sum;
        var changeSum = true;
        isIgnored = false;
        var length = 0;
        while (input[length] != '}')
        {
            length ++;
            length += ParseString(input[length..], ref sum, ignoreRed, false, out _);
            length ++;
            length += Parse(input[length..], ref sum, ignoreRed, ignoreRed, out isIgnored);
            if (isIgnored)
            {
                isIgnored = false;
                changeSum = false;
            }
            if (input[length] is ',')
                length++;
        }
        if (!changeSum)
            sum = originalSum;
        return length + 1;
    }

    private static int ParseString(ReadOnlySpan<char> input, ref int sum, bool ignoreRed, bool isInObject, out bool isIgnored)
    {
        var length = 0;
        while (input[length] != '"')
            length++;
        length++;
        isIgnored = (isInObject && input[..length] is ['r', 'e', 'd', '"']);
        return length;
    }

    private static int ParseInt(ReadOnlySpan<char> input, ref int sum, bool ignoreRed, bool isInObject, out bool isIgnored)
    {
        isIgnored = false;
        var length = 0;
        if (input[0] is '-')
            length++;

        var number = 0;
        while (length < input.Length && input[length] is >= '0' and <= '9')
        {
            number = number * 10 + (input[length] - '0');
            length++;
        }

        if (input[0] is '-')
            sum += -number;
        else
            sum += number;

        return length;
    }

    public object PartTwo(string input)
    => Parse(input, true);
}
