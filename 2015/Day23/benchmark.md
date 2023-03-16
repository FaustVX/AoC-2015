``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |     Mean |    Error |   StdDev |    Gen0 |   Gen1 | Allocated |
|-------- |---------:|---------:|---------:|--------:|-------:|----------:|
| PartOne | 69.16 μs | 1.371 μs | 3.010 μs | 13.0615 |      - |  160.6 KB |
| PartTwo | 93.66 μs | 1.818 μs | 3.088 μs | 18.9209 | 0.1221 | 231.85 KB |
