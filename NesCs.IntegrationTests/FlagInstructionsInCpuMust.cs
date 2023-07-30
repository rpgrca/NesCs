using NesCs.Tests.Common;

namespace NesCs.IntegrationTests;

public class FlagInstructionsInCpuMust
{
    [Theory]
    [ProcessorFileTestData("18")]
    [ProcessorFileTestData("38")]
    [ProcessorFileTestData("58")]
    [ProcessorFileTestData("b8")]
    [ProcessorFileTestData("d8")]
    public void Execute10000FlagTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}