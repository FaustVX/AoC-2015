``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |     Mean |     Error |    StdDev |   Median |   Gen0 | Allocated |
|-------- |---------:|----------:|----------:|---------:|-------:|----------:|
| PartOne | 7.021 μs | 0.1259 μs | 0.0983 μs | 6.987 μs |      - |      24 B |
| PartTwo | 1.717 μs | 0.0342 μs | 0.0833 μs | 1.746 μs | 0.0019 |      24 B |
