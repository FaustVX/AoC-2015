``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |    Mean |  Error | StdDev |         Gen0 |       Gen1 |      Gen2 | Allocated |
|-------- |--------:|-------:|-------:|-------------:|-----------:|----------:|----------:|
| PartOne | 207.8 s | 1.44 s | 1.20 s | 9040000.0000 | 80000.0000 | 2000.0000 | 105.63 GB |
| PartTwo | 211.3 s | 4.12 s | 6.42 s | 9040000.0000 | 81000.0000 | 2000.0000 | 105.63 GB |
