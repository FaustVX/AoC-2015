#nullable enable
namespace AdventOfCode.Y2015.Day12;

[ProblemName("JSAbacusFramework.io")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var sum = 0;
        Parse(input, ref sum);
        return sum;
    }

    private static int Parse(ReadOnlySpan<char> input, ref int sum)
    => input[0] switch
    {
        '[' => ParseArray(input[1..], ref sum) + 1,
        '{' => ParseObject(input[1..], ref sum) + 1,
        '"' => ParseString(input[1..], ref sum) + 1,
        '-' or (>= '0' and <= '9') => ParseInt(input[0..], ref sum),
        _ => throw new UnreachableException()
    };

    private static int ParseArray(ReadOnlySpan<char> input, ref int sum)
    {
        var length = 0;
        while (input[length] != ']')
        {
            length += Parse(input[length..], ref sum);
            if (input[length] is ',')
                length++;
        }
        return length + 1;
    }

    private static int ParseObject(ReadOnlySpan<char> input, ref int sum)
    {
        var length = 0;
        while (input[length] != '}')
        {
            length ++;
            length += ParseString(input[length..], ref sum);
            length ++;
            length += Parse(input[length..], ref sum);
            if (input[length] is ',')
                length++;
        }
        return length + 1;
    }

    private static int ParseString(ReadOnlySpan<char> input, ref int sum)
    {
        var length = 0;
        while (input[length] != '"')
            length++;
        return length + 1;
    }

    private static int ParseInt(ReadOnlySpan<char> input, ref int sum)
    {
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
    {
        return 0;
    }
}
