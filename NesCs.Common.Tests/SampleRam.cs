using System.Text.Json.Serialization;
using NesCs.Common.Tests.Converters;

namespace NesCs.Common.Tests;

[JsonConverter(typeof(SampleRamConverter))]
public readonly struct SampleRam
{
    public int Address { get; init; }
    public byte Value { get; init; }
}