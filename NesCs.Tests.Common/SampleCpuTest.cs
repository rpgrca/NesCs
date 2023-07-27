using System.Diagnostics;
using System.Text.Json;

namespace NesCs.Tests.Common;

[DebuggerDisplay("{Name}")]
public readonly struct SampleCpuTest
{
    public string? Name { get; init; }
    public SampleCpuTestStatus? Initial { get; init; }
    public SampleCpuTestStatus? Final { get; init; }
    public JsonElement[][] Cycles { get; init; }
}