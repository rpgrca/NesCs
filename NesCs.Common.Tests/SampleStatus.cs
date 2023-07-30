using System.Text.Json.Serialization;
using NesCs.Common.Tests.Converters;

namespace NesCs.Common.Tests;

[JsonConverter(typeof(SampleStatusConverter))]
public readonly struct SampleStatus
{
    public byte S { get; init; }
    public byte P { get; init; }
    public int PC { get; init; }
    public byte X { get; init; }
    public byte Y { get; init; }
    public byte A { get; init; }
    [JsonConverter(typeof(SampleRamArrayConverter))]
    public SampleRam[] RAM { get; init; }
}