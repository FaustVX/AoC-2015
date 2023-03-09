#nullable enable
using System.Text.Json;
namespace AdventOfCode.Y2015.Day12;

[ProblemName("JSAbacusFramework.io")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var json = JsonDocument.Parse(input);
        return Sum(json.RootElement);
    }

    private static int Sum(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Array:
            {
                var i = 0;
                foreach (var item in element.EnumerateArray())
                    i += Sum(item);
                return i;
            }
            case JsonValueKind.Object:
            {
                var i = 0;
                foreach (var item in element.EnumerateObject())
                    i += Sum(item.Value);
                return i;
            }
            case JsonValueKind.Number:
                return element.GetInt32();
            default:
                return 0;
        }
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}
