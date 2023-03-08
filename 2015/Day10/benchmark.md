``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |      Mean |     Error |    StdDev |      Gen0 |      Gen1 |      Gen2 | Allocated |
|-------- |----------:|----------:|----------:|----------:|----------:|----------:|----------:|
| PartOne |  5.314 ms | 0.1047 ms | 0.0979 ms |  828.1250 |  828.1250 |  828.1250 |   6.09 MB |
| PartTwo | 72.604 ms | 0.8347 ms | 0.7808 ms | 8875.0000 | 8875.0000 | 5875.0000 |  84.06 MB |
