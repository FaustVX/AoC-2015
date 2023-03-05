#nullable enable
namespace AdventOfCode.Y2015.Day08;

[ProblemName("Matchsticks")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var totalSpace = 0;
        foreach (var line in input.AsMemory().SplitLine().AsEnumerable())
        {
            var space = -1; // for the last "
            for (var i = 1; i < line.Length; i++)
            {
                switch (line.Span[i..])
                {
                    case ['\\', 'x', ..]:
                        space += 1;
                        i += 3;
                        break;
                    case ['\\', ..]:
                        space += 1;
                        i += 1;
                        break;
                    default:
                        space++;
                        break;
                }
            }
            totalSpace += line.Length - space;
        }
        return totalSpace;
    }

    public object PartTwo(string input)
    {
        var totalSpace = 0;
        foreach (var line in input.AsMemory().SplitLine().AsEnumerable())
        {
            var space = 2; // for the 2 " at the start and end
            for (var i = 0; i < line.Length; i++)
            {
                switch (line.Span[i])
                {
                    case '\\':
                    case '"':
                        space += 2;
                        break;
                    default:
                        space++;
                        break;
                }
            }
            totalSpace += space - line.Length;
        }
        return totalSpace;
    }
}
