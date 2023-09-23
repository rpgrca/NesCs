using static NesCs.Roms.IntegrationTests.Utilities;

namespace NesCs.Roms.IntegrationTests;

public class PpuOpenBusMust
{
    // TODO: On 1:1 CPU:PPU takes 3 second, now takes 8
    [Theory]
    [InlineData("ppu_open_bus/ppu_open_bus.nes", 0xE755, "\nppu_open_bus\n\nPassed\n")] // ticks 89315269
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