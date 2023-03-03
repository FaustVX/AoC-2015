#nullable enable
namespace AdventOfCode.Y2015.Day01;

[ProblemName("Not Quite Lisp")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var floor = 0;
        foreach (var c in input)
            floor += c switch
            {
                '(' => +1,
                ')' => -1,
                _ => throw new UnreachableException(),
            };
        return floor;
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}
