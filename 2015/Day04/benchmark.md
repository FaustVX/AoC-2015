``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |        Mean |     Error |    StdDev | Allocated |
|-------- |------------:|----------:|----------:|----------:|
| PartOne |    66.56 ms |  1.173 ms |  1.682 ms |     312 B |
| PartTwo | 2,354.92 ms | 12.429 ms | 11.626 ms |     688 B |
