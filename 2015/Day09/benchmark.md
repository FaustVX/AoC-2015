``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |     Mean |    Error |   StdDev |      Gen0 | Allocated |
|-------- |---------:|---------:|---------:|----------:|----------:|
| PartOne | 41.66 ms | 0.832 ms | 1.166 ms | 2416.6667 |  29.52 MB |
| PartTwo | 42.08 ms | 0.436 ms | 0.386 ms | 2416.6667 |  29.52 MB |
