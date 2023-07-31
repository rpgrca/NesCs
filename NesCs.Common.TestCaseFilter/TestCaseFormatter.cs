using System.Linq;
using System.Text;

using NesCs.Logic.Cpu;

namespace NesCs.Common.TestCaseFilter;

public class TestCaseFormatter
{
    private readonly StringBuilder _builder;

    public TestCaseFormatter(string opcode, Dictionary<int, string> cases)
    {
        _builder = new StringBuilder();
        _builder.AppendLine($"    public static IEnumerable<object[]> Opcode{opcode}JsonFeeder()");
        _builder.AppendLine("    {");

        foreach (var value in cases.OrderBy(p => p.Key))
        {
            var ps = value.Key == 0
                ? ""
                : new string(((Cpu6502.ProcessorStatus)value.Key).ToString().Replace(" ", "").Replace(",", "").Reverse().ToArray());

            _builder.AppendLine($$""""        /* {{value.Key,3}} {{ps,-4}} */ yield return new object[] { JsonDeserializer.Deserialize("""{{value.Value}}""") };"""");
        }

        _builder.AppendLine("    }");
    }

    public override string ToString() => _builder.ToString();
}