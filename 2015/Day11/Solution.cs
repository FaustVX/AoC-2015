#nullable enable
namespace AdventOfCode.Y2015.Day11;

[ProblemName("Corporate Policy")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        Span<char> span = stackalloc char[input.Length];
        input.AsSpan().CopyTo(span);
        do
        {
            Increment(span);
        } while (!IsValid(span));
        return span.ToString();
    }

    private static bool IsValid(Span<char> span)
    {
        var straight3Letters = false;
        var countPairs = 0;
        var lastPairPosition = -1;
        for (var i = 0; i < span.Length; i++)
        {
            if (span[i] is 'i' or 'o' or 'l')
            {
                span[i]++; // increment for speedup
                span.Slice(i + 1).Fill('a');
                return IsValid(span);
            }
            if (!straight3Letters && i <= span.Length - 3 && span.Slice(i, 3) is [var a, var b, var c] && a == b - 1 && b == c - 1)
                straight3Letters = true;
            if (countPairs < 2 && i <= span.Length - 2 && i - lastPairPosition >= 2 && span.Slice(i, 2) is [var d, var e] && d == e)
            {
                countPairs++;
                lastPairPosition = i;
            }
        }
        return straight3Letters && countPairs >= 2;
    }

    private static void Increment(Span<char> input)
    {
        if (input[^1] != 'z')
            input[^1]++;
        else
        {
            Increment(input[..^1]);
            input[^1] = 'a';
        }
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}
