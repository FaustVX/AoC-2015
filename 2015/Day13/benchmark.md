``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |      Mean |    Error |   StdDev |       Gen0 | Allocated |
|-------- |----------:|---------:|---------:|-----------:|----------:|
| PartOne |  52.28 ms | 0.621 ms | 0.485 ms |  3300.0000 |  40.62 MB |
| PartTwo | 513.35 ms | 8.179 ms | 7.651 ms | 32000.0000 | 387.43 MB |
