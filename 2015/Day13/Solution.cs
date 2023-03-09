#nullable enable
namespace AdventOfCode.Y2015.Day13;

// Copied from [Day09](../Day09/Solution.cs)
[ProblemName("Knights of the Dinner Table")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        ParseInput(input);
        return CreatePath(ImmutableList.CreateRange(Person.People.Values), ImmutableStack<Person>.Empty).Max(CalculateTotalHappiness);
    }

    private int CalculateTotalHappiness(ImmutableStack<Person> table)
    {
        return Inner(table, table.Peek());

        static int Inner(ImmutableStack<Person> table, Person first)
        {
            if (table.IsEmpty)
                return 0;
            table = table.Pop(out var current);
            if (table.IsEmpty)
                return current.HappinessWith(first) + first.HappinessWith(current);
            return current.HappinessWith(table.Peek()) + table.Peek().HappinessWith(current) + Inner(table, first);
        }
    }

    private static IEnumerable<ImmutableStack<Person>> CreatePath(ImmutableList<Person> locations, ImmutableStack<Person> path)
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
        Person.People.Clear();
        Happiness.Happinesses.Clear();
#pragma warning disable CS0612 //ObsoleteAttribute
        foreach (var line in input.SplitLine())
#pragma warning restore CS0612
        {
            (var person1, (_, (var gain_lose, (var happiness, (_, (_, (_, (_, (_, (_, (var person2, _))))))))))) = line.Split(' ');
            Happiness.Happinesses[(Person.People[person1], Person.People[person2[..^1]])] = gain_lose is "lose" ? -int.Parse(happiness) : int.Parse(happiness);
        }
    }

    public object PartTwo(string input)
    {
        ParseInput(input);
        foreach (var person in Person.People.Values.ToImmutableArray())
        {
            Happiness.Happinesses[(Person.People["Me"], person)] = 0;
            Happiness.Happinesses[(person, Person.People["Me"])] = 0;
        }
        return CreatePath(ImmutableList.CreateRange(Person.People.Values), ImmutableStack<Person>.Empty).Max(CalculateTotalHappiness);
    }
}

sealed record Person(string Name)
{
    public static DefaultableDictionary<string, Person> People { get; } = new(name => new(name));

    public int HappinessWith(Person? other)
    => other is null || other == this ? 0 : Happiness.Happinesses[(this, other)];

    public override int GetHashCode()
    => Name.GetHashCode();

    public override string? ToString()
    => Name;
}

sealed record class Happiness(Person Person1, Person Person2)
{
    public static Dictionary<Happiness, int> Happinesses { get; } = new();
    public bool Equals(Happiness? other)
    => other is { Person1: var p1, Person2: var p2 } && Person1 == p1 && Person2 == p2;

    public override int GetHashCode()
    => (Person1, Person2).GetHashCode();

    public override string? ToString()
    => $"{Person1} - {Person2}";

    public static implicit operator Happiness((Person loc1, Person loc2) tuple)
    => new(tuple.loc1, tuple.loc2);
}
