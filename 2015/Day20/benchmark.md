``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |      Mean |     Error |   StdDev | Allocated |
|-------- |----------:|----------:|---------:|----------:|
| PartOne | 788.98 ms | 10.231 ms | 9.069 ms |     408 B |
| PartTwo |  93.29 ms |  1.243 ms | 1.038 ms |      88 B |
