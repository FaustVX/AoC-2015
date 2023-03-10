#nullable enable
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Y2015.Day17;


// Copied from [Day15](../Day15/Solution.cs)
[ProblemName("No Such Thing as Too Much")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var barrels = ParseInput(input);
        var sets = Combine(barrels, Globals.IsTestInput ? 25 : 150);
        return sets.Count();
    }

    private static List<Barrel> ParseInput(string input)
    => input.AsMemory()
        .SplitLine()
        .AsEnumerable()
        .Select(static line => int.Parse(line.Span))
        .Select(static (v, id) => new Barrel() { Volume = v, Id = id })
        .ToList();

    private static HashSet<ImmutableHashSet<Barrel>> Combine(List<Barrel> barrels, int remainingLiters)
    {
        var sets = new HashSet<ImmutableHashSet<Barrel>>(capacity: barrels.Count, comparer: EqualityComparer.Instance);
        foreach (var barrel in barrels)
            Inner(ImmutableList.CreateRange(barrels).Remove(barrel), sets, ImmutableHashSet.Create(barrel), remainingLiters - barrel.Volume);
        return sets;

        static void Inner(ImmutableList<Barrel> barrels, HashSet<ImmutableHashSet<Barrel>> sets, ImmutableHashSet<Barrel> currentSet, int remainingLiters)
        {
            barrels = barrels.RemoveRange(barrels.Where(b => b.Volume > remainingLiters));
            if (remainingLiters == 0)
            {
                sets.Add(currentSet);
                return;
            }
            if (barrels.IsEmpty || remainingLiters < 0)
                return;
            foreach (var barrel in barrels)
                Inner(ImmutableList.CreateRange(barrels).Remove(barrel), sets, currentSet.Add(barrel), remainingLiters - barrel.Volume);
        }
    }

    public object PartTwo(string input)
    {
        var barrels = ParseInput(input);
        var sets = Combine(barrels, Globals.IsTestInput ? 25 : 150);
        return sets.GroupBy(static set => set.Count()).MinBy(static g => g.Key)!.Count();
    }
}
class Barrel
{
    public required int Id { get; init; }
    public required int Volume { get; init; }
    public override string ToString()
    => $"{Volume} ({Id})";
}

class EqualityComparer : IEqualityComparer<ImmutableHashSet<Barrel>>
{
    public static EqualityComparer Instance { get; } = new();

    private EqualityComparer()
    { }

    public bool Equals(ImmutableHashSet<Barrel>? x, ImmutableHashSet<Barrel>? y)
    {
        if ((x, y) is (null, null))
            return true;
        if (y is not null)
            return x?.SetEquals(y) ?? false;
        return false;
    }

    public int GetHashCode([DisallowNull] ImmutableHashSet<Barrel> obj)
    => obj.Sum(static b => b.Volume);
}

