```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.4.1 (25E253) [Darwin 25.4.0]
Apple M5 Pro, 1 CPU, 15 logical and 15 physical cores
.NET SDK 10.0.201
  [Host]     : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a
  DefaultJob : .NET 10.0.5 (10.0.5, 10.0.526.15411), Arm64 RyuJIT armv8.0-a


```
| Method             | SetSize | MatchCase | Mean           | Error       | StdDev      | Gen0   | Gen1   | Allocated |
|------------------- |-------- |---------- |---------------:|------------:|------------:|-------:|-------:|----------:|
| **IntersectAny**       | **Large**   | **End**       |  **3,523.4529 ns** |   **9.8303 ns** |   **9.1952 ns** | **0.2327** |      **-** |    **1968 B** |
| ListAnyContains    | Large   | End       |  6,950.9632 ns |   6.5796 ns |   5.4943 ns |      - |      - |         - |
| HashSetAnyContains | Large   | End       |    117.6305 ns |   0.2344 ns |   0.2078 ns |      - |      - |         - |
| ForeachEarlyExit   | Large   | End       | 31,382.2350 ns |  64.7133 ns |  54.0385 ns |      - |      - |         - |
| **IntersectAny**       | **Large**   | **None**      |  **3,437.4509 ns** |   **3.0764 ns** |   **2.7272 ns** | **0.2327** |      **-** |    **1968 B** |
| ListAnyContains    | Large   | None      |  7,867.7969 ns | 140.4400 ns | 131.3676 ns |      - |      - |         - |
| HashSetAnyContains | Large   | None      |    112.0303 ns |   1.4558 ns |   1.2905 ns |      - |      - |         - |
| ForeachEarlyExit   | Large   | None      | 31,377.3384 ns |  74.7288 ns |  58.3434 ns |      - |      - |         - |
| **IntersectAny**       | **Large**   | **Start**     |    **290.6825 ns** |   **1.6974 ns** |   **1.4174 ns** | **0.2351** | **0.0014** |    **1968 B** |
| ListAnyContains    | Large   | Start     |      1.1232 ns |   0.0019 ns |   0.0018 ns |      - |      - |         - |
| HashSetAnyContains | Large   | Start     |      1.1387 ns |   0.0021 ns |   0.0020 ns |      - |      - |         - |
| ForeachEarlyExit   | Large   | Start     |      0.3293 ns |   0.0257 ns |   0.0240 ns |      - |      - |         - |
| **IntersectAny**       | **Medium**  | **End**       |    **407.3878 ns** |   **4.1000 ns** |   **3.6345 ns** | **0.0744** |      **-** |     **624 B** |
| ListAnyContains    | Medium  | End       |    141.8666 ns |   0.2824 ns |   0.2504 ns |      - |      - |         - |
| HashSetAnyContains | Medium  | End       |     21.6727 ns |   0.1405 ns |   0.1245 ns |      - |      - |         - |
| ForeachEarlyExit   | Medium  | End       |    717.1482 ns |   2.4599 ns |   1.9206 ns |      - |      - |         - |
| **IntersectAny**       | **Medium**  | **None**      |    **401.7419 ns** |   **6.4549 ns** |   **6.0379 ns** | **0.0744** |      **-** |     **624 B** |
| ListAnyContains    | Medium  | None      |    141.1782 ns |   0.9700 ns |   0.9074 ns |      - |      - |         - |
| HashSetAnyContains | Medium  | None      |     22.6132 ns |   0.0503 ns |   0.0420 ns |      - |      - |         - |
| ForeachEarlyExit   | Medium  | None      |    784.0134 ns |  14.6461 ns |  15.0405 ns |      - |      - |         - |
| **IntersectAny**       | **Medium**  | **Start**     |     **79.0611 ns** |   **0.0708 ns** |   **0.0627 ns** | **0.0745** | **0.0001** |     **624 B** |
| ListAnyContains    | Medium  | Start     |      1.1287 ns |   0.0007 ns |   0.0007 ns |      - |      - |         - |
| HashSetAnyContains | Medium  | Start     |      1.1505 ns |   0.0013 ns |   0.0011 ns |      - |      - |         - |
| ForeachEarlyExit   | Medium  | Start     |      0.3160 ns |   0.0046 ns |   0.0041 ns |      - |      - |         - |
| **IntersectAny**       | **Small**   | **End**       |     **47.1819 ns** |   **0.3894 ns** |   **0.3252 ns** | **0.0363** |      **-** |     **304 B** |
| ListAnyContains    | Small   | End       |      3.9833 ns |   0.1028 ns |   0.1184 ns |      - |      - |         - |
| HashSetAnyContains | Small   | End       |      3.2257 ns |   0.0064 ns |   0.0053 ns |      - |      - |         - |
| ForeachEarlyExit   | Small   | End       |      5.7588 ns |   0.1299 ns |   0.1152 ns |      - |      - |         - |
| **IntersectAny**       | **Small**   | **None**      |     **46.6630 ns** |   **0.0781 ns** |   **0.0730 ns** | **0.0363** |      **-** |     **304 B** |
| ListAnyContains    | Small   | None      |      3.7866 ns |   0.0057 ns |   0.0048 ns |      - |      - |         - |
| HashSetAnyContains | Small   | None      |      2.9023 ns |   0.0191 ns |   0.0179 ns |      - |      - |         - |
| ForeachEarlyExit   | Small   | None      |      5.7933 ns |   0.0098 ns |   0.0082 ns |      - |      - |         - |
| **IntersectAny**       | **Small**   | **Start**     |     **35.7541 ns** |   **0.0611 ns** |   **0.0571 ns** | **0.0363** |      **-** |     **304 B** |
| ListAnyContains    | Small   | Start     |      1.1317 ns |   0.0215 ns |   0.0201 ns |      - |      - |         - |
| HashSetAnyContains | Small   | Start     |      1.1521 ns |   0.0099 ns |   0.0078 ns |      - |      - |         - |
| ForeachEarlyExit   | Small   | Start     |      0.2997 ns |   0.0180 ns |   0.0169 ns |      - |      - |         - |
