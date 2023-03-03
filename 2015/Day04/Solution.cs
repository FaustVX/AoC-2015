#nullable enable
using System.Security.Cryptography;

namespace AdventOfCode.Y2015.Day04;

[ProblemName("The Ideal Stocking Stuffer")]
public class Solution : Solver //, IDisplay
{
    delegate bool IsValid(Span<byte> span);
    public object PartOne(string input)
    => Execute(input, static destination => destination is [0x00, 0x00, <= 0x0f, ..]);

    static int Execute(string input, IsValid isValid)
    {
        ReadOnlySpan<byte> nines = stackalloc byte[]
        {
            (byte)'9',
            (byte)'9',
            (byte)'9',
            (byte)'9',
            (byte)'9',
            (byte)'9',
            (byte)'9',
            (byte)'9',
            (byte)'9',
        };
        Span<byte> destination = stackalloc byte[16];
        Span<byte> source = new byte[input.Length + 1];
        Encoding.UTF8.GetBytes(input, source);
        for (var i = 0; i < int.MaxValue; i++)
        {
            ToSpan(i, source.Slice(input.Length));
            MD5.HashData(source, destination);
            if (isValid(destination))
                return i;
            if (source.Slice(input.Length).SequenceEqual(nines[..source.Slice(input.Length).Length]))
                Enlarge(ref source);
        }

        throw new UnreachableException();

        static void Enlarge(ref Span<byte> source)
        {
            var span = new byte[source.Length + 1];
            source.CopyTo(span);
            source = span;
        }

        static int ToSpan(int i, Span<byte> span)
        {
            switch (i)
            {
                case < 0:
                    throw new UnreachableException();
                case < 10:
                    WriteInt(i, ref span[0]);
                    return 1;
                default:
                    var written = ToSpan(i / 10, span);
                    WriteInt(i % 10, ref span[written]);
                    return written + 1;
            }

            static void WriteInt(int i, ref byte value)
            => value = (byte)(((byte)i) + '0');
        }
    }

    public object PartTwo(string input)
    => Execute(input, static destination => destination is [0x00, 0x00, 0x00, ..]);

}
