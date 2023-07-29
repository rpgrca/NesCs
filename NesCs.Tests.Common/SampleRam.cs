using System.Text.Json.Serialization;
using NesCs.Tests.Common.Converters;

namespace NesCs.Tests.Common;

[JsonConverter(typeof(SampleRamConverter))]
public class SampleRam
{
    public int Address;
    public byte Value;
}
