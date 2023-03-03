#nullable enable
namespace AdventOfCode.Y2015.Day05;

[ProblemName("Doesn't He Have Intern-Elves For This?")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        return input.AsMemory().SplitLine().AsEnumerable().Count(IsNice);

        static bool IsNice(ReadOnlyMemory<char> input)
        {
            var vowelsCount = 0;
            var hasDouble = false;
            var hasForbidden = false;

            for (var i = 0; i < input.Length; i++)
            {
                if (input.Span[i] is 'a' or 'e' or 'i' or 'o' or 'u')
                    vowelsCount++;
                if (i >= 1)
                {
                    if (input.Span[i - 1] == input.Span[i])
                        hasDouble = true;
                    if (input.Span[(i - 1)..] is ['a', 'b', ..] or ['c', 'd', ..] or ['p', 'q', ..] or ['x', 'y', ..])
                        hasForbidden = true;
                }
            }

            return vowelsCount >= 3 && hasDouble && !hasForbidden;
        }
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}
