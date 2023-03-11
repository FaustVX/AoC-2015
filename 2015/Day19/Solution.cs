#nullable enable
namespace AdventOfCode.Y2015.Day19;

[ProblemName("Medicine for Rudolph")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var (replacements, molecule) = ParseInput(input.AsMemory());
        return Replace1(replacements, molecule).Count;
    }

    private static (IReadOnlyDictionary<string, IReadOnlyList<string>> replacements, string molecule) ParseInput(ReadOnlyMemory<char> input)
    {
#pragma warning disable CS0612 //ObsoleteAttribute
        if (input.Split2Lines().Span is not [var rep, var molecule])
            throw new UnreachableException();
#pragma warning restore CS0612

        var replacements = rep
            .SplitLine()
            .AsEnumerable()
            .Select(ParseReplacement)
            .GroupBy(static t => t.input, static t => t.outputs)
            .ToDictionary(static g => g.Key, static g => (IReadOnlyList<string>)g.ToList());
        return (replacements, new(molecule.Span));
    }

    private static HashSet<string> Replace1(IReadOnlyDictionary<string, IReadOnlyList<string>> replacements, string molecule)
    {
        var set = new HashSet<string>();
        foreach (var (input, outputs) in replacements)
            foreach (var output in outputs)
            {
                var lastPos = 0;
                while ((lastPos = molecule.IndexOf(input, lastPos)) != -1)
                    set.Add($"{molecule[..lastPos]}{output}{molecule[(lastPos += input.Length)..]}");
            }
        return set;
    }

    private static (string input, string outputs) ParseReplacement(ReadOnlyMemory<char> line)
    {
        if (line.Split(" => ").Span is [var input, var output])
            return (new(input.Span), new(output.Span));
        throw new UnreachableException();
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}
