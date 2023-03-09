``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |      Mean |    Error |    StdDev |   Gen0 | Allocated |
|-------- |----------:|---------:|----------:|-------:|----------:|
| PartOne |  41.76 μs | 0.808 μs |  0.931 μs | 0.6104 |   8.12 KB |
| PartTwo | 344.33 μs | 6.775 μs | 14.291 μs | 8.3008 | 105.89 KB |
