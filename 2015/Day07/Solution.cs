#nullable enable
namespace AdventOfCode.Y2015.Day07;

[ProblemName("Some Assembly Required")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var wireA = new Wire()
        {
            Name = "a",
        };
        var wires = new DefaultableDictionary<string, Wire>(static name => new() { Name = name })
        {
            [wireA.Name] = wireA,
        };

#pragma warning disable CS0612 //ObsoleteAttribute
        foreach (var line in input.SplitLine())
#pragma warning restore CS0612
        {
            switch (line.Split(' '))
            {
                case ["NOT", var from, _, var to]:
                    wires[to].From = new NotOperation()
                    {
                        Wire = GetWire(wires, from),
                    };
                    break;
                case [var left, "AND", var right, _, var to]:
                    wires[to].From = new AndOperation()
                    {
                        Left = GetWire(wires, left),
                        Right = GetWire(wires, right),
                    };
                    break;
                case [var left, "OR", var right, _, var to]:
                    wires[to].From = new OrOperation()
                    {
                        Left = GetWire(wires, left),
                        Right = GetWire(wires, right),
                    };
                    break;
                case [var left, "LSHIFT", var offset, _, var to]:
                    wires[to].From = new LeftShiftOperation()
                    {
                        Wire = GetWire(wires, left),
                        Shift = byte.Parse(offset),
                    };
                    break;
                case [var left, "RSHIFT", var offset, _, var to]:
                    wires[to].From = new RightShiftOperation()
                    {
                        Wire = GetWire(wires, left),
                        Shift = byte.Parse(offset),
                    };
                    break;
                case [var value, _, var to]:
                    if (ushort.TryParse(value, out var val))
                        wires[to].From = new ValueOperation()
                        {
                            Value = val,
                        };
                    else
                        wires[to].From = new WireOperation()
                        {
                            Wire = GetWire(wires, value),
                        };
                    break;
                default:
                    throw new UnreachableException();
            }
        }

        return wireA.Execute();

        static Wire GetWire(IReadOnlyDictionary<string, Wire> wires, string name)
        {
            var wire = wires[name];
            if (ushort.TryParse(name, out var val))
                wire.From ??= new ValueOperation()
                {
                    Value = val
                };
            return wire;
        }
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}

sealed class Wire
{
    public required string Name { get; init; }
    public Operation From { get; set; } = null!;
    private ushort? _value = null;
    public ushort Execute() => _value ??= From.Execute();
    public override string ToString()
    => Name;
}

abstract class Operation
{
    public abstract ushort Execute();
    public override abstract string ToString();
}

sealed class NotOperation : Operation
{
    public required Wire Wire { get; init; }
    public override ushort Execute()
    => (ushort)(~Wire.Execute());

    public override string ToString()
    => $"NOT {Wire}";
}

sealed class AndOperation : Operation
{
    public required Wire Left { get; init; }
    public required Wire Right { get; init; }
    public override ushort Execute()
    => (ushort)(Left.Execute() & Right.Execute());

    public override string ToString()
    => $"{Left} AND {Right}";
}

sealed class OrOperation : Operation
{
    public required Wire Left { get; init; }
    public required Wire Right { get; init; }
    public override ushort Execute()
    => (ushort)(Left.Execute() | Right.Execute());

    public override string ToString()
    => $"{Left} OR {Right}";
}

sealed class LeftShiftOperation : Operation
{
    public required Wire Wire { get; init; }
    public required byte Shift { get; init; }
    public override ushort Execute()
    => (ushort)(Wire.Execute() << Shift);

    public override string ToString()
    => $"{Wire} LSHIFT {Shift}";
}

sealed class RightShiftOperation : Operation
{
    public required Wire Wire { get; init; }
    public required byte Shift { get; init; }
    public override ushort Execute()
    => (ushort)(Wire.Execute() >> Shift);

    public override string ToString()
    => $"{Wire} RSHIFT {Shift}";
}

sealed class WireOperation : Operation
{
    public required Wire Wire { get; init; }
    public override ushort Execute()
    => Wire.Execute();

    public override string ToString()
    => $"{Wire}";
}

sealed class ValueOperation : Operation
{
    public required ushort Value { get; init; }
    public override ushort Execute()
    => Value;

    public override string ToString()
    => $"{Value}";
}
