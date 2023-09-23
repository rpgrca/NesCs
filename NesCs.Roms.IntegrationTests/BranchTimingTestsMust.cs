using static NesCs.Roms.IntegrationTests.Utilities;

namespace NesCs.Roms.IntegrationTests;

public class BranchTimingTestsMust
{
    [Theory]
    [InlineData("branch_timing_tests/1.Branch_Basics.nes", 0xE1B9, "BRANCH TIMING BASICSPASSED")]
    [InlineData("branch_timing_tests/2.Backward_Branch.nes", 0xE17A, "BACKWARD BRANCH TIMINGPASSED")]
    [InlineData("branch_timing_tests/3.Forward_Branch.nes", 0xE17C, "FORWARD BRANCH TIMINGPASSED")]
    public void BeExecutedCorrectly(string romName, int printAddress, string expectedResult)
    {
        var message = string.Empty;
        var ram = new byte[0x10000];
        var clock = CreateSetup(ram, romName, 0xE4F0, printAddress, (cpu, _) =>
        {
            message += (char)cpu.ReadByteFromAccumulator();
        });

        clock.Run();
        Assert.Equal(expectedResult, message);
    }
}