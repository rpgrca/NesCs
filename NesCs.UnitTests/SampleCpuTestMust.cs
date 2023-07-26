using System.Text.Json;

namespace NesCs.UnitTests;

public class Cpu6502ProcessorMust
{
    [Fact]
    public void ExecuteSampleTestCorrectly()
    {
        const string jsonText = """
{
	"name": "b1 28 b5",
	"initial": {
		"pc": 59082,
		"s": 39,
		"a": 57,
		"x": 33,
		"y": 174,
		"p": 96,
		"ram": [
			[59082, 177],
			[59083, 40],
			[59084, 181],
			[40, 160],
			[41, 233],
			[59982, 119]
		]
	},
	"final": {
		"pc": 59084,
		"s": 39,
		"a": 119,
		"x": 33,
		"y": 174,
		"p": 96,
		"ram": [
			[40, 160],
			[41, 233],
			[59082, 177],
			[59083, 40],
			[59084, 181],
			[59982, 119]
		]
	},
	"cycles": [
		[59082, 177, "read"],
		[59083, 40, "read"],
		[40, 160, "read"],
		[41, 233, "read"],
		[59083, 40, "read"],
		[59982, 119, "read"]
	]
}
""";

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var trace = JsonSerializer.Deserialize<SampleCpuTest>(jsonText, options);
        var sut = new Cpu6502.Builder()
            .RunningProgram(new byte[] { 0xb1, 0x28 })
            .WithRamSizeOf(0xffff)
            .WithStackPointerAt(trace.Initial.Value.S)
            .WithProcessorStatusAs(trace.Initial.Value.P)
            .WithXAs(trace.Initial.Value.X)
            .WithYAs(trace.Initial.Value.Y)
            .WithProgramCounterAs(trace.Initial.Value.PC)
            .RamPatchedAs(trace.Initial.Value.RAM)
            .Build();

		sut.Run();
    }
}