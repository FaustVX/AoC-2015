``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |     Mean |   Error |   StdDev |   Gen0 | Allocated |
|-------- |---------:|--------:|---------:|-------:|----------:|
| PartOne | 445.1 μs | 8.58 μs | 10.54 μs | 6.8359 |  86.13 KB |
| PartTwo | 438.2 μs | 5.51 μs |  4.88 μs | 6.8359 |  86.13 KB |
