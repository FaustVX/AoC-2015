``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |     Mean |   Error |   StdDev |   Median |   Gen0 | Allocated |
|-------- |---------:|--------:|---------:|---------:|-------:|----------:|
| PartOne | 230.6 μs | 5.22 μs | 15.15 μs | 224.8 μs | 0.9766 |  15.75 KB |
| PartTwo | 347.2 μs | 6.90 μs | 13.30 μs | 341.9 μs | 0.9766 |  15.75 KB |
