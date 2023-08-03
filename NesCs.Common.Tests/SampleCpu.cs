using System.Text.Json.Serialization;
using NesCs.Common.Tests.Converters;

namespace NesCs.Common.Tests;

[JsonConverter(typeof(SampleCpuConverter))]
public readonly struct SampleCpu
{
    public string Name { get; init; }
    public SampleStatus Initial { get; init; }
    public SampleStatus Final { get; init; }
    [JsonConverter(typeof(SampleCycleArrayConverter))]
    public SampleCycle[] Cycles { get; init; }

    public override string ToString() => Name;
}