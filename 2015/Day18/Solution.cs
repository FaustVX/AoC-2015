#nullable enable
using CommunityToolkit.HighPerformance;

namespace AdventOfCode.Y2015.Day18;

[ProblemName("Like a GIF For Your Yard")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var (light, bak) = ParseInput(input.AsMemory().SplitLine());
        var size = light.GetLength(0);
        var steps = Globals.IsTestInput ? 4 : 100;
        for (int step = 0; step < steps; step++)
            NextStep(ref light, ref bak);
        return Count(light);
    }

    private static void NextStep(ref bool[,] light, ref bool[,] next)
    {
        ReadOnlySpan2D<bool> span = light;
        var size = span.Width;
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
            {
                next[x, y] = (light[x, y], Count(Slice3x3(span, x, y))) switch
                {
                    (true, var count) => (count - 1) is 2 or 3, // -1 to remove light[x, y] (which is on)
                    (false, var count) => count is 3,
                };
            }
        (light, next) = (next, light);

        static ReadOnlySpan2D<bool> Slice3x3(ReadOnlySpan2D<bool> span, int x, int y)
        {
            var (width, height) = (3, 3);

            if (x <= 0)
                (x, width) = (0, 2);
            else if (x >= span.Width - 1)
                (x, width) = (x - 1, 2);
            else
                x--;

            if (y <= 0)
                (y, height) = (0, 2);
            else if (y >= span.Height - 1)
                (y, height) = (y - 1, 2);
            else
                y--;

            return span.Slice(x, y, width, height);
        }
    }

    private static int Count(ReadOnlySpan2D<bool> span)
    {
        var count = 0;
        foreach (var light in span)
            if (light)
                count++;
        return count;
    }

    private static (bool[,] light, bool[,] bak) ParseInput(ReadOnlyMemory<ReadOnlyMemory<char>> input)
    {
        var size = Globals.IsTestInput ? 6 : 100;
        var light = new bool[size, size];
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
                light[x, y] = input.Span[y].Span[x] is '#';
        return (light, new bool[size, size]);
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}
