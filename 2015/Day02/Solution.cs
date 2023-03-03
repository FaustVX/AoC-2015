#nullable enable
namespace AdventOfCode.Y2015.Day02;

[ProblemName("I Was Told There Would Be No Math")]
public class Solution : Solver //, IDisplay
{
    readonly record struct Box(int L, int W, int H)
    {
        private int Side1 => L*W;
        private int Side2 => W*H;
        private int Side3 => H*L;
        public int Surface => 2*Side1 + 2*Side2 + 2*Side3 + Math.Min(Side1, Math.Min(Side2, Side3));
        public static Box Parse(ReadOnlyMemory<char> input)
        {
            var parts = input.Split("x");
            return new(int.Parse(parts.Span[0].Span), int.Parse(parts.Span[1].Span), int.Parse(parts.Span[2].Span));
        }
    }
    public object PartOne(string input)
    => input.AsMemory()
        .SplitLine()
        .AsEnumerable()
        .Select(Box.Parse)
        .Sum(static box => box.Surface);

    public object PartTwo(string input)
    {
        return 0;
    }
}
