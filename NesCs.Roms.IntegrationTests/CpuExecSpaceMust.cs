using static NesCs.Roms.IntegrationTests.Utilities;

namespace NesCs.Roms.IntegrationTests;

public class CpuExecSpaceMust
{
    [Theory]
    [InlineData("cpu_exec_space/test_cpu_exec_space_ppuio.nes", 0xE7F5, "\u001b[0;37mTEST:test_cpu_exec_space_ppuio\n\u001b[0;33mThis program verifies that the\nCPU can execute code from any\npossible location that it can\naddress, including I/O space.\n\nIn addition, it will be tested\nthat an RTS instruction does a\ndummy read of the byte that\nimmediately follows the\ninstructions.\n\n\u001b[0;37m\u001b[1;34mJSR+RTS TEST OK\nJMP+RTS TEST OK\nRTS+RTS TEST OK\nJMP+RTI TEST OK\nJMP+BRK TEST OK\n\u001b[0;37m\nPassed\n")] // ticks: 3293317
    [InlineData("cpu_exec_space/test_cpu_exec_space_apu.nes", 0xE976, "", Skip = "Must implement APU \u001b[0;37mTEST: test_cpu_exec_space_apu\n\u001b[0;33mThis program verifies that the\nCPU can execute code from any\npossible location that it can\naddress, including I/O space.\n\nIn this test, it is also\nverified that not only all\nwrite-only APU I/O ports\nreturn the open bus, but\nalso the unallocated I/O\nspace in $4018..$40FF.\n\n\u001b[0;37m\u001b[1;34m0022 \b\b\b\b\b4000 \u001b[0;37mERROR\n\u001b[0;33mMysteriously Landed at $0234\u001b[0;37m\nProgram flow did not follow\nthe planned path, for a number\nof different possible reasons.\n\u001b[0;37m\nFailure To Obey Predetermined Execution Path\n\nFailed #2\n")]
    public void BeExecutedCorrectly(string romName, int powerOffAddress, string expectedResult)
    {
        var ram = new byte[0x10000];
        var clock = CreateSetup(ram, romName, powerOffAddress);
        clock.Run();

        var result = GetString(ram);
        Assert.Equal(0, ram[0x6000]);
        Assert.Equal(expectedResult, result);
    }

    private static string GetString(byte[] ram) =>
        System.Text.Encoding.ASCII.GetString(ram[0x6004..].TakeWhile(p => !p.Equals(0)).ToArray());
}