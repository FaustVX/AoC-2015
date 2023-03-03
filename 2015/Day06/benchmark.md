``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |     Mean |    Error |   StdDev |      Gen0 |     Gen1 |     Gen2 | Allocated |
|-------- |---------:|---------:|---------:|----------:|---------:|---------:|----------:|
| PartOne | 41.46 ms | 0.550 ms | 0.872 ms | 2083.3333 | 250.0000 | 250.0000 |  23.97 MB |
| PartTwo | 47.39 ms | 0.932 ms | 1.211 ms | 2083.3333 | 250.0000 | 250.0000 |  23.97 MB |
