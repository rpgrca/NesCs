using System.Text.Json.Serialization;
using NesCs.Tests.Common.Converters;

namespace NesCs.Tests.Common;

[JsonConverter(typeof(SampleCpuConverter))]
public class SampleCpu
{
    public string Name;
    public byte[] Opcodes;
    public SampleStatus Initial;
    public SampleStatus Final;
    [JsonConverter(typeof(SampleCycleArrayConverter))]
    public SampleCycle[] Cycles;

    public SampleCpu()
    {
        Name = string.Empty;
        Opcodes = Array.Empty<byte>();
        Cycles = Array.Empty<SampleCycle>();
        Initial = new SampleStatus();
        Final = new SampleStatus();
    }
}
