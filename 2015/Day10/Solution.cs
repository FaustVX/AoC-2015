#nullable enable
namespace AdventOfCode.Y2015.Day10;

[ProblemName("Elves Look, Elves Say")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        for (var round = 0; round < 40; round++)
        {
            var sb = new StringBuilder();
            var last = input[0];
            var count = 1;
            for (var i = 1; i < input.Length; i++)
            {
                if (input[i] == last)
                    count++;
                else
                {
                    sb.Append(count).Append(last);
                    last = input[i];
                    count = 1;
                }
            }
            sb.Append(count).Append(last);
            input = sb.ToString();
        }
        return checked(input.Length);
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}
