using System.Diagnostics;
using System.Text.Json.Serialization;
using NesCs.Common.Tests.Converters;

namespace NesCs.Common.Tests;

[DebuggerDisplay("{Address}, {Value}, {Type}")]
[JsonConverter(typeof(SampleCycleConverter))]
public readonly struct SampleCycle
{
    public int Address { get; init; }
    public byte Value { get; init; }
    public string Type { get; init; }
}