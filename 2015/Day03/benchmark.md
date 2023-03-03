``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |     Mean |   Error |  StdDev |    Gen0 |   Gen1 | Allocated |
|-------- |---------:|--------:|--------:|--------:|-------:|----------:|
| PartOne | 193.3 μs | 2.74 μs | 2.56 μs | 12.2070 | 2.9297 | 150.59 KB |
| PartTwo | 210.7 μs | 1.17 μs | 1.10 μs | 12.2070 | 2.9297 | 150.59 KB |
