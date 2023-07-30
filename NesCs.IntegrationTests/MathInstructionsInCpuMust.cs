using NesCs.Tests.Common;

namespace NesCs.IntegrationTests;

public class MathInstructionsInCpuMust
{
    [Theory]
    [ProcessorFileTestData("69")]
    [ProcessorFileTestData("6d")]
    [ProcessorFileTestData("e9")]
    public void Execute10000MathTestsPerOpcodeCorrectly(SampleCpu sampleCpu)
    {
        var trace = new List<(int, byte, string)>();
		var sut = Utilities.CreateSubjectUnderTestFromSample(sampleCpu.Opcodes, sampleCpu.Initial, trace);
        sut.Run();

        Utilities.Equal(sampleCpu.Final, sut);
        Utilities.Equal(sampleCpu.Cycles, trace);
    }
}