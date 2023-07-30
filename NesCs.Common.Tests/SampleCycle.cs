using System.Text.Json.Serialization;
using NesCs.Common.Tests.Converters;

namespace NesCs.Common.Tests;

[JsonConverter(typeof(SampleCycleConverter))]
public readonly struct SampleCycle
{
    public int Address { get; init; }
    public byte Value { get; init; }
    public string Type { get; init; }
}