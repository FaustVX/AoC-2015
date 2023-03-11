#nullable enable
namespace AdventOfCode.Y2015.Day20;

[ProblemName("Infinite Elves and Infinite Houses")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var requestedPresents = int.Parse(input);
        var house = 1;
        var maxElves = (int)Math.Sqrt(house) + 1;
        while (SumOfPresents(house, maxElves, 10) < requestedPresents)
            house += 1;
        return house;
    }

    private static int SumOfPresents(int house, int maxElves, int qtyPerElf)
    {
        var sum = 0;
        for (var elf = 1; elf <= maxElves; elf++)
            if (house % elf == 0)
            {
                sum += elf;
                sum += house / elf;
            }
        return sum * qtyPerElf;
    }

    public object PartTwo(string input)
    {
        var requestedPresents = int.Parse(input);
        var house = 1;
        while (SumOfPresents(house, 50, 11) < requestedPresents)
            house += 1;
        return house;
    }
}
