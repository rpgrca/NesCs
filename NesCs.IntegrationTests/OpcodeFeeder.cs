using System.Text.Json;
using System.Collections;
using NesCs.Tests.Common;

namespace NesCs.IntegrationTests;

public class OpcodeFeeder<T> : IEnumerable<object[]> where T : IOpcodeFile 
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var jsonText = File.ReadAllText(T.Filename);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var datas = JsonSerializer.Deserialize<SampleCpuTest[]>(jsonText, options);

        foreach (var data in datas)
        {
            yield return new object[] { data };
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class OpcodeA0 : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/a0.json"; }

public class OpcodeA1 : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/a1.json"; }

public class OpcodeA2 : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/a2.json"; }

public class OpcodeA4 : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/a4.json"; }

public class OpcodeA5 : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/a5.json"; }

public class OpcodeA6 : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/a6.json"; }

public class OpcodeA9 : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/a9.json"; }

public class OpcodeAD : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/ad.json"; }

public class OpcodeAE : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/ae.json"; }

public class OpcodeB1 : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/b1.json"; }

public class OpcodeB4 : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/b4.json"; }

public class OpcodeB5 : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/b5.json"; }

public class OpcodeB6 : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/b6.json"; }

public class OpcodeB9 : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/b9.json"; }

public class OpcodeBD : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/bd.json"; }

public class OpcodeBE : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/be.json"; }