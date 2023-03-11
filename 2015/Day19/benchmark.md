``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |     Mean |     Error |    StdDev |     Gen0 |    Gen1 |  Allocated |
|-------- |---------:|----------:|----------:|---------:|--------:|-----------:|
| PartOne | 1.550 ms | 0.0307 ms | 0.0315 ms | 113.2813 | 82.0313 | 1401.66 KB |
| PartTwo | 6.728 ms | 0.1165 ms | 0.1090 ms |  31.2500 |       - |  415.91 KB |
