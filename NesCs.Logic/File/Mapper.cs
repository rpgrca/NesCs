using System.Diagnostics;

namespace NesCs.Logic.File;

[DebuggerDisplay("{DebuggerDisplay}")]
public struct Mapper
{
    public int Number { get; init; }
    public int StartAddress { get; init; }
    public int EndAddress { get; init; }

    private readonly string DebuggerDisplay => $"Mapper {Number} ({StartAddress:X4}-{EndAddress:X4})";
}
