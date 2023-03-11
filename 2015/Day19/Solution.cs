#nullable enable
namespace AdventOfCode.Y2015.Day19;

[ProblemName("Medicine for Rudolph")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var (replacements, molecule) = ParseInputPart1(input.AsMemory());
        return Replace1(replacements, molecule).Count;
    }

    private static (IReadOnlyDictionary<string, IReadOnlyList<string>> replacements, string molecule) ParseInputPart1(ReadOnlyMemory<char> input)
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

    private static (IReadOnlyList<(string from, string to)> replacements, char[] requestedMolecule) ParseInputPart2(ReadOnlyMemory<char> input)
    {
#pragma warning disable CS0612 //ObsoleteAttribute
        if (input.Split2Lines().Span is not [var rep, var molecule])
            throw new UnreachableException();
#pragma warning restore CS0612

        var replacements = rep
            .SplitLine()
            .AsEnumerable()
            .Select(ParseReplacement)
            .Select(static t => (t.outputs, t.input))
            .OrderByDescending(static t => t.outputs.Length)
            .ToArray();

        var mol = new char[molecule.Length];
        molecule.CopyTo(mol);

        return (replacements, mol);
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

    private static bool Replace(Span<char> molecule, ReadOnlySpan<char> from, ReadOnlySpan<char> to)
    {
        for (var i = from.Length; i <= molecule.Length; i++)
            if (molecule.Slice(i - from.Length, from.Length) is var slice && slice.SequenceEqual(from))
            {
                var offset = from.Length - to.Length;
                foreach (ref var c in slice)
                {
                    c = to[0];
                    (_, to) = to;
                    if (to.IsEmpty)
                        break;
                }
                if (offset is 0)
                    return true;
                for (var x = i; x < molecule.Length; x++)
                {
                    molecule[i - offset] = molecule[i];
                    molecule[i] = '\0';
                }
                molecule[^1] = '\0';
                return true;
            }
        return false;
    }

    public object PartTwo(string input)
    {
        (var replacements, Span<char> molecule) = ParseInputPart2(input.AsMemory());

        for (var i = 0;; i++)
        {
            if (molecule is ['e'])
                return i;
            if (molecule is [])
                throw new UnreachableException();
            foreach (var (from, to) in replacements)
            {
                if (Replace(molecule, from, to))
                {
                    molecule = molecule.TrimEnd('\0');
                    break;
                }
            }
        }
    }
}
