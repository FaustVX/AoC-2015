``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1574/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


```
|  Method |      Mean |     Error |    StdDev | Allocated |
|-------- |----------:|----------:|----------:|----------:|
| PartOne |  4.699 ms | 0.0919 ms | 0.1257 ms |      43 B |
| PartTwo | 16.891 ms | 0.3336 ms | 0.4784 ms |      52 B |
