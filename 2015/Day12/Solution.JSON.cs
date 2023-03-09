#nullable enable
using System.Text.Json;
namespace AdventOfCode.Y2015.Day12;

[ProblemName("JSAbacusFramework.io")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var json = JsonDocument.Parse(input);
        return Sum(json.RootElement, false);
    }

    private static int Sum(JsonElement element, bool ignoreRed)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Array:
            {
                var i = 0;
                foreach (var item in element.EnumerateArray())
                    i += Sum(item, ignoreRed);
                return i;
            }
            case JsonValueKind.Object:
            {
                var i = 0;
                foreach (var item in element.EnumerateObject())
                    if (ignoreRed && item.Value.ValueKind is JsonValueKind.String && item.Value.GetString() is "red")
                        return 0;
                    else
                        i += Sum(item.Value, ignoreRed);
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
        var json = JsonDocument.Parse(input);
        return Sum(json.RootElement, true);
    }
}
