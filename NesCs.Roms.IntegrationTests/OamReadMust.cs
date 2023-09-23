using static NesCs.Roms.IntegrationTests.Utilities;

namespace NesCs.Roms.IntegrationTests;

public class OamReadMust
{
    [Theory]
    [InlineData("oam_read/oam_read.nes", 0xE755, "----------------\n----------------\n----------------\n----------------\n----------------\n----------------\n----------------\n----------------\n----------------\n----------------\n----------------\n----------------\n----------------\n----------------\n----------------\n----------------\n\noam_read\n\nPassed\n")] // ticks 2809549
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