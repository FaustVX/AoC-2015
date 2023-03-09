#nullable enable
using System.Runtime.CompilerServices;

namespace AdventOfCode.Y2015.Day16;

[ProblemName("Aunt Sue")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var aunts = Parseinput(input);
        return aunts.First(static a => a.IsValidForPart1(3, 7, 2, 3, 0, 0, 5, 3, 2, 1)).Id;
    }

    private static List<AuntSue> Parseinput(ReadOnlySpan<char> input)
    {
        var aunts = new  List<AuntSue>(capacity: 500);
        for (int i = 1; i < 500; i++)
        {
            var endOfLine = input.IndexOf('\n');
            var line = input[..endOfLine];
            aunts.Add(new(i, Find(line, "children"), Find(line, "cats"), Find(line, "samoyeds"), Find(line, "pomeranians"), Find(line, "akitas"), Find(line, "vizslas"), Find(line, "goldfish"), Find(line, "trees"), Find(line, "cars"), Find(line, "perfumes")));
            input = input[(endOfLine + 1)..];
        }
        aunts.Add(new(500, Find(input, "children"), Find(input, "cats"), Find(input, "samoyeds"), Find(input, "pomeranians"), Find(input, "akitas"), Find(input, "vizslas"), Find(input, "goldfish"), Find(input, "trees"), Find(input, "cars"), Find(input, "perfumes")));

        return aunts;

        static int? Find(ReadOnlySpan<char> line, string value)
        {
            var start = line.IndexOf(value);
            if (start == -1)
                return null;
            start += value.Length + 2;
            var end = line[start..].IndexOf(' ');
            if (end != -1)
                return int.Parse(line.Slice(start, end - 1));
            else
                return int.Parse(line.Slice(start));
        }
    }

    public object PartTwo(string input)
    {
        var aunts = Parseinput(input);
        return aunts.First(static a => a.IsValidForPart2(3, 7, 2, 3, 0, 0, 5, 3, 2, 1)).Id;
    }
}

public record AuntSue(int Id, int? Children, int? Cats, int? Samoyeds, int? Pomeranians, int? Akitas, int? Vizslas, int? Goldfish, int? Trees, int? Cars, int? Perfumes)
{
    public bool IsValidForPart1(int children, int cats, int samoyeds, int pomeranians, int akitas, int vizslas, int goldfish, int trees, int cars, int perfumes)
    {
        return IsEquals(Children, children)
            && IsEquals(Cats, cats)
            && IsEquals(Samoyeds, samoyeds)
            && IsEquals(Pomeranians, pomeranians)
            && IsEquals(Akitas, akitas)
            && IsEquals(Vizslas, vizslas)
            && IsEquals(Goldfish, goldfish)
            && IsEquals(Trees, trees)
            && IsEquals(Cars, cars)
            && IsEquals(Perfumes, perfumes);

        static bool IsEquals(int? property, int value)
        => property is null || property == value;
    }
    public bool IsValidForPart2(int children, int cats, int samoyeds, int pomeranians, int akitas, int vizslas, int goldfish, int trees, int cars, int perfumes)
    {
        return IsEquals(Children, children)
            && IsGreater(Cats, cats)
            && IsEquals(Samoyeds, samoyeds)
            && IsLess(Pomeranians, pomeranians)
            && IsEquals(Akitas, akitas)
            && IsEquals(Vizslas, vizslas)
            && IsLess(Goldfish, goldfish)
            && IsGreater(Trees, trees)
            && IsEquals(Cars, cars)
            && IsEquals(Perfumes, perfumes);

        static bool IsEquals(int? property, int value)
        => property is null || property == value;

        static bool IsGreater(int? property, int value)
        => property is null || property > value;

        static bool IsLess(int? property, int value)
        => property is null || property < value;
    }
}
