using static NesCs.Roms.IntegrationTests.Utilities;

namespace NesCs.Roms.IntegrationTests;

public class CpuResetMust
{
    [Theory]
    [InlineData("cpu_reset/ram_after_reset.nes", 0xE29C, 0xE9D5, "\nram_after_reset\n\nPassed\n")] // ticks: 57819145
    [InlineData("cpu_reset/registers.nes", 0xE29C, 0xE9D5, "A  X  Y  P  S\n34 56 78 FF 0F \n\nregisters\n\nPassed\n")] // ticks: 57487249
    public void ReturnPassed(string romName, int resetAddress, int poweroffAddress, string expectedResult)
    {
        var ram = new byte[0x10000];
        var clock = CreateSetup(ram, romName, poweroffAddress, resetAddress);
        clock.Run();

        var result = GetString(ram);
        Assert.Equal(0, ram[0x6000]);
        Assert.Equal(expectedResult, result);
    }

    private static string GetString(byte[] ram) =>
        System.Text.Encoding.ASCII.GetString(ram[0x6004..].TakeWhile(p => !p.Equals(0)).ToArray());
}