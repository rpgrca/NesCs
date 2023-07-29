using System.Text.Json.Serialization;
using NesCs.Tests.Common.Converters;

namespace NesCs.Tests.Common;

[JsonConverter(typeof(SampleStatusConverter))]
public class SampleStatus
{
    public byte S;
    public byte P;
    public int PC;
    public byte X;
    public byte Y;
    public byte A;
    [JsonConverter(typeof(SampleRamArrayConverter))]
    public SampleRam[] RAM;

    public SampleStatus()
    {
        RAM = Array.Empty<SampleRam>();
    }
}