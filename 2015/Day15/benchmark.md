``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |     Mean |   Error |   StdDev |   Median |      Gen0 |      Gen1 |      Gen2 | Allocated |
|-------- |---------:|--------:|---------:|---------:|----------:|----------:|----------:|----------:|
| PartOne | 270.5 ms | 5.39 ms |  6.21 ms | 270.0 ms | 8000.0000 | 7500.0000 | 1500.0000 |  84.68 MB |
| PartTwo | 270.0 ms | 8.46 ms | 24.67 ms | 261.5 ms | 8000.0000 | 7500.0000 | 1500.0000 |  84.68 MB |
