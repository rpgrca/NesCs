using NesCs.Tests.Common;

namespace NesCs.IntegrationTests;

public class FlagInstructionsInCpuMust
{
    [Theory]
    [ProcessorFileTestData("18")]
    public void Execute10000FlagTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}