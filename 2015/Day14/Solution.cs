#nullable enable
namespace AdventOfCode.Y2015.Day14;

[ProblemName("Reindeer Olympics")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var reindeers = ParseInput(input);
        for (var second = 0; second < 2503; second++)
            foreach (var reindeer in reindeers)
                reindeer.NextSecond(second);
        return reindeers.Max(static r => r.DistanceTraveled);
    }

    private static List<Reindeer> ParseInput(string input)
    {
#pragma warning disable CS0612 //ObsoleteAttribute
        var lines = input.SplitLine();
#pragma warning restore CS0612
        var reindeers = new List<Reindeer>(capacity: lines.Length);
        foreach (var line in lines)
        {
            (var name, (_, (_, (var speed, (_, (_, (var flight, (_, (_, (_, (_, (_, (_, (var rest, _)))))))))))))) = line.Split(' ');
            reindeers.Add(new(name, int.Parse(speed), int.Parse(flight), int.Parse(rest)));
        }
        return reindeers;
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}

record class Reindeer(string Name, int Speed, int FlightDuration, int RestDuration)
{
    public int DistanceTraveled { get; private set; }
    private readonly int _durationCycle = FlightDuration + RestDuration;
    public void NextSecond(int totalSecond)
    {
        if (totalSecond % _durationCycle < FlightDuration)
            DistanceTraveled += Speed;
    }
}
