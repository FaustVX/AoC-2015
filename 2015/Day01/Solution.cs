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
        var floor = 0;
        for (var i = 0; i < input.Length; i++)
        {
            floor += input[i] switch
            {
                '(' => +1,
                ')' => -1,
                _ => throw new UnreachableException(),
            };
            if (floor == -1)
                return i + 1;
        }
        throw new UnreachableException();
    }
}
