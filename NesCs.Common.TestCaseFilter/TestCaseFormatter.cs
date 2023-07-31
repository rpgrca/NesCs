using System.Linq;
using System.Text;
using System.Numerics;

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

        var max = cases.Keys.Max(p => BitOperations.PopCount((uint)p));
        var formatNumber = $"/* {{0,3}} ";
        var formatFlags = $"{{0,{-max}}} */ ";

        foreach (var value in cases.OrderBy(p => p.Key))
        {
            var ps = value.Key == 0
                ? ""
                : new string(((Cpu6502.ProcessorStatus)value.Key).ToString().Replace(" ", "").Replace(",", "").Reverse().ToArray());

            _builder.Append("        ");
            _builder.Append(string.Format(formatNumber, value.Key));
            _builder.Append(string.Format(formatFlags, ps));
            _builder.Append("yield return new object[] { JsonDeserializer.Deserialize(\"\"\"");
            _builder.Append(value.Value);
            _builder.AppendLine("\"\"\") };");
        }

        _builder.AppendLine("    }");
    }

    public override string ToString() => _builder.ToString();
}