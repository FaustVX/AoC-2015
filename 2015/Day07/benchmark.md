``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |     Mean |    Error |   StdDev |    Gen0 |   Gen1 | Allocated |
|-------- |---------:|---------:|---------:|--------:|-------:|----------:|
| PartOne | 87.26 μs | 0.996 μs | 0.931 μs | 11.8408 | 2.3193 |  145.8 KB |
| PartTwo | 92.21 μs | 1.534 μs | 1.435 μs | 11.8408 | 2.4414 | 145.89 KB |
