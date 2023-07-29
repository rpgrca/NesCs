using System.Text.Json.Serialization;
using NesCs.Tests.Common.Converters;

namespace NesCs.Tests.Common;

[JsonConverter(typeof(SampleCycleConverter))]
public class SampleCycle
{
    public int Address;
    public byte Value;
    public string Type;

    public SampleCycle()
    {
        Type = string.Empty;
    }
}
