#nullable enable
namespace AdventOfCode.Y2015.Day09;

[ProblemName("All in a Single Night")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        ParseInput(input);
        var locations = ImmutableList.CreateRange(Location.Locations.Values);
        return CreatePath(locations, ImmutableStack<Location>.Empty).Min(CalculatePathLength);
    }

    private int CalculatePathLength(ImmutableStack<Location> path)
    {
        if (path.IsEmpty)
            return 0;
        path = path.Pop(out var first);
        if (path.IsEmpty)
            return 0;
        return first.ValueFrom(path.Peek()) + CalculatePathLength(path);
    }

    private static IEnumerable<ImmutableStack<Location>> CreatePath(ImmutableList<Location> locations, ImmutableStack<Location> path)
    {
        if (locations.IsEmpty)
            yield return path;
        else
            foreach (var location in locations)
                foreach (var p in CreatePath(locations.Remove(location), path.Push(location)))
                    yield return p;
    }

    private static void ParseInput(string input)
    {
        Location.Locations.Clear();
        Distance.Distances.Clear();
#pragma warning disable CS0612 //ObsoleteAttribute
        foreach (var line in input.SplitLine())
#pragma warning restore CS0612
        {
            (var loc1, (_, (var loc2, (_, (var dist, _))))) = line.Split(' ');
            Distance.Distances[(Location.Locations[loc1], Location.Locations[loc2])] = int.Parse(dist);
        }
    }

    public object PartTwo(string input)
    {
        ParseInput(input);
        var locations = ImmutableList.CreateRange(Location.Locations.Values);
        return CreatePath(locations, ImmutableStack<Location>.Empty).Max(CalculatePathLength);
    }
}

sealed record Location(string Name)
{
    public static DefaultableDictionary<string, Location> Locations { get; } = new(name => new(name));

    public int ValueFrom(Location? other)
    => other is null || other == this ? 0 : Distance.Distances[(this, other)];

    public override int GetHashCode()
    => Name.GetHashCode();

    public override string? ToString()
    => Name;
}

sealed record class Distance(Location Location1, Location Location2)
{
    public static Dictionary<Distance, int> Distances { get; } = new();
    public bool Equals(Distance? other)
    => other is { Location1: var l1, Location2: var l2 } && (Location1 == l1 || Location1 == l2) && (Location2 == l1 || Location2 == l2);

    public override int GetHashCode()
    => Location1.GetHashCode() * Location2.GetHashCode();

    public override string? ToString()
    => $"{Location1} - {Location2}";

    public static implicit operator Distance((Location loc1, Location loc2) tuple)
    => new(tuple.loc1, tuple.loc2);
}
