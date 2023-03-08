#nullable enable
namespace AdventOfCode.Y2015.Day10;

[ProblemName("Elves Look, Elves Say")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        for (int i = 0; i < 40; i++)
            input = LookandSay(input);
        return input.Length;
    }

    private static string LookandSay(string input)
    {
        return LookandSay(input.AsSpan(), new()).ToString();

        static StringBuilder LookandSay(ReadOnlySpan<char> input, StringBuilder output)
        {
            if (input.Length == 1)
                return output.Append($"1{input[0]}");
            if (input.Length == 0)
                return output;
            var i = 1;
            var count = 1;
            for (; i < input.Length; i++)
                if (input[i] == input[0])
                    count++;
                else
                    return LookandSay(input[i..], output.Append($"{count}{input[0]}"));
            return output;
        }
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}
