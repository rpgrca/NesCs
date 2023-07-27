using System.Text.Json;
using System.Collections.Generic;
using System.Collections;
using NesCs.Tests.Common;

namespace NesCs.IntegrationTests;

public class OpcodeFeeder<T> : IEnumerable<object[]> where T : IOpcodeFile 
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var jsonText = System.IO.File.ReadAllText(T.Filename);
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

public class OpcodeB5 : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/b5.json"; }

public class OpcodeB1 : IOpcodeFile { public static string Filename => "../../../../../ProcessorTests/nes6502/v1/b1.json"; }
