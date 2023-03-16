#nullable enable
namespace AdventOfCode.Y2015.Day23;

[ProblemName("Opening the Turing Lock")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var cpu = new Computer();
#pragma warning disable CS0612 //ObsoleteAttribute
        cpu.Run(input.SplitLine());
#pragma warning restore CS0612
        return Globals.IsTestInput ? cpu.RegA.Value : cpu.RegB.Value;
    }

    public object PartTwo(string input)
    {
        var cpu = new Computer();
        cpu.RegA.Value = 1;
#pragma warning disable CS0612 //ObsoleteAttribute
        cpu.Run(input.SplitLine());
#pragma warning restore CS0612
        return Globals.IsTestInput ? cpu.RegA.Value : cpu.RegB.Value;
    }
}

class Computer
{
    public class Register
    {
        public ulong Value { get; set; }

        public override string ToString()
        => Value.ToString();
    }

    public Register RegA { get; set; } = new();
    public Register RegB { get; set; } = new();
    public int PC { get; set; }

    public void Run(IReadOnlyList<string> lines)
    {
        while (true)
        {
            if (PC < 0 || PC >= lines.Count)
                return;
            switch (lines[PC++].Split(" ,".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                case ["hlf", var reg]:
                    GetRegister(reg).Value /= 2;
                    break;
                case ["tpl", var reg]:
                    GetRegister(reg).Value *= 3;
                    break;
                case ["inc", var reg]:
                    GetRegister(reg).Value++;
                    break;
                case ["jmp", var offset]:
                    PC += int.Parse(offset) - 1; // -1 because PC was incremented
                    break;
                case ["jie", var reg, var offset]:
                    if (GetRegister(reg).Value % 2 == 0)
                        PC += int.Parse(offset) - 1; // -1 because PC was incremented
                    break;
                case ["jio", var reg, var offset]:
                    if (GetRegister(reg).Value == 1)
                        PC += int.Parse(offset) - 1; // -1 because PC was incremented
                    break;
            }
        }
    }

    private Register GetRegister(string reg)
    => reg switch
    {
        "a" => RegA,
        "b" => RegB,
        _ => throw new UnreachableException(),
    };
}
