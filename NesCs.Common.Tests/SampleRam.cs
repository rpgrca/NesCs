using System.Text.Json.Serialization;
using NesCs.Tests.Common.Converters;

namespace NesCs.Tests.Common;

[JsonConverter(typeof(SampleRamConverter))]
public readonly struct SampleRam
{
    public int Address { get; init; }
    public byte Value { get; init; }
}