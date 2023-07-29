using System.Text.Json.Serialization;
using NesCs.Tests.Common.Converters;

namespace NesCs.Tests.Common;

[JsonConverter(typeof(SampleCycleConverter))]
public readonly struct SampleCycle
{
    public int Address { get; init; }
    public byte Value { get; init; }
    public string Type { get; init; }
}