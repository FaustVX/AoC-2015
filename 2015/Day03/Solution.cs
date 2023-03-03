#nullable enable
namespace AdventOfCode.Y2015.Day03;

[ProblemName("Perfectly Spherical Houses in a Vacuum")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var pos = (x: 0, y: 0);
        var positions = new HashSet<(int x, int y)>
        {
            pos,
        };
        foreach (var c in input)
        {
            switch (c)
            {
                case '^':
                    pos.y--;
                    break;
                case 'v':
                    pos.y++;
                    break;
                case '<':
                    pos.x--;
                    break;
                case '>':
                    pos.x++;
                    break;
                default:
                    throw new UnreachableException();
            }
            positions.Add(pos);
        }
        return positions.Count;
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}
