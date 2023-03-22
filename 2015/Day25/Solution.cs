#nullable enable
namespace AdventOfCode.Y2015.Day25;

[ProblemName("Let It Snow")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var row = input.IndexOf("row");
        var col = input.IndexOf("column", row);

        row = int.Parse(input.AsSpan()[(row + 4)..input.IndexOf(',', row + 4)]);
        col = int.Parse(input.AsSpan()[(col + 7)..input.IndexOf('.', col + 7)]);

        var count = GetValueFromXY(row, col);

        var previous = 20151125;
        for (var i = 1; i < count; i++)
            previous = (int)((previous * 252533L) % 33554393);
        return previous;
    }

    private static int GetValueFromXY(int r, int c)
    {
        var col1AtRow = Enumerable.Range(1, r - 1).Sum() + 1;
        return Enumerable.Repeat(r, c - 1).Select(static (r, c) => (r + c + 1)).Aggregate(col1AtRow, static (acc, c) => acc + c);
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}
