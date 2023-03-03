#nullable enable
namespace AdventOfCode.Y2015.Day06;

[ProblemName("Probably a Fire Hazard")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var light = new bool[1000, 1000];
#pragma warning disable CS0612 //ObsoleteAttribute
        foreach (var line in input.SplitLine())
#pragma warning restore CS0612 //
            switch (line.Split(" ,".ToCharArray()))
            {
                case ["toggle", var x1, var y1, _, var x2, var y2]:
                    Toggle(light, int.Parse(x1), int.Parse(y1), int.Parse(x2), int.Parse(y2));
                    break;
                case [_, "on", var x1, var y1, _, var x2, var y2]:
                    TurnOn(light, int.Parse(x1), int.Parse(y1), int.Parse(x2), int.Parse(y2));
                    break;
                case [_, "off", var x1, var y1, _, var x2, var y2]:
                    TurnOff(light, int.Parse(x1), int.Parse(y1), int.Parse(x2), int.Parse(y2));
                    break;
                default:
                    throw new UnreachableException();
            }

        return light.Cast<bool>().Count(static l => l);

        static void Toggle(bool[,] light, int x1, int y1, int x2, int y2)
        {
            for (var x = x1; x <= x2; x++)
                for (var y = y1; y <= y2; y++)
                    light[x, y] ^= true; // a ^ true == !a
        }

        static void TurnOn(bool[,] light, int x1, int y1, int x2, int y2)
        {
            for (var x = x1; x <= x2; x++)
                for (var y = y1; y <= y2; y++)
                    light[x, y] = true;
        }

        static void TurnOff(bool[,] light, int x1, int y1, int x2, int y2)
        {
            for (var x = x1; x <= x2; x++)
                for (var y = y1; y <= y2; y++)
                    light[x, y] = false;
        }
    }

    public object PartTwo(string input)
    {
        var light = new byte[1000, 1000];
#pragma warning disable CS0612 //ObsoleteAttribute
        foreach (var line in input.SplitLine())
#pragma warning restore CS0612 //
            switch (line.Split(" ,".ToCharArray()))
            {
                case ["toggle", var x1, var y1, _, var x2, var y2]:
                    Toggle(light, int.Parse(x1), int.Parse(y1), int.Parse(x2), int.Parse(y2));
                    break;
                case [_, "on", var x1, var y1, _, var x2, var y2]:
                    TurnOn(light, int.Parse(x1), int.Parse(y1), int.Parse(x2), int.Parse(y2));
                    break;
                case [_, "off", var x1, var y1, _, var x2, var y2]:
                    TurnOff(light, int.Parse(x1), int.Parse(y1), int.Parse(x2), int.Parse(y2));
                    break;
                default:
                    throw new UnreachableException();
            }

        return light.Cast<byte>().Sum(static l => l);

        static void Toggle(byte[,] light, int x1, int y1, int x2, int y2)
        {
            for (var x = x1; x <= x2; x++)
                for (var y = y1; y <= y2; y++)
                    light[x, y] += 2;
        }

        static void TurnOn(byte[,] light, int x1, int y1, int x2, int y2)
        {
            for (var x = x1; x <= x2; x++)
                for (var y = y1; y <= y2; y++)
                    light[x, y]++;
        }

        static void TurnOff(byte[,] light, int x1, int y1, int x2, int y2)
        {
            for (var x = x1; x <= x2; x++)
                for (var y = y1; y <= y2; y++)
                {
                    ref var l = ref light[x, y];
                    if (l > 0)
                        l--;
                }
        }
    }
}
