#nullable enable
namespace AdventOfCode.Y2015.Day20;

[ProblemName("Infinite Elves and Infinite Houses")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var requestedPresents = int.Parse(input);
        var house = 1;
        while (SumOfPresents(house) < requestedPresents)
            house += 1;
        return house;
    }

    private static int SumOfPresents(int house)
    {
        var sum = 0;
        var d = (int)Math.Sqrt(house) + 1;
        for (var elf = 1; elf <= d; elf++)
            if (house % elf == 0)
            {
                sum += elf;
                sum += house / elf;
            }
        return sum * 10;
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}
