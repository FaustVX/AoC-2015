``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |     Mean |    Error |   StdDev |   Gen0 | Allocated |
|-------- |---------:|---------:|---------:|-------:|----------:|
| PartOne | 63.88 μs | 1.085 μs | 0.962 μs | 0.3662 |   4.81 KB |
| PartTwo | 60.03 μs | 0.513 μs | 0.428 μs | 0.3662 |   4.81 KB |
