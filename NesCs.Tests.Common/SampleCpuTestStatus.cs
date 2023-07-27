namespace NesCs.Tests.Common;

public readonly struct SampleCpuTestStatus
{
    public int PC { get; init; }
    public byte S { get; init; }
    public byte A { get; init; }
    public byte X { get; init; }
    public byte Y { get; init; }
    public byte P { get; init; }
    public int[][] RAM { get; init; }
}