using static NesCs.Roms.IntegrationTests.Utilities;

namespace NesCs.Roms.IntegrationTests;

public class VblNmiTimingMust
{
    [Theory]
    [InlineData("vbl_nmi_timing/1.frame_basics.nes", 0xE589, 0xE217, "PPU FRAME BASICSPASSED\0")]
    [InlineData("vbl_nmi_timing/2.vbl_timing.nes", 0xE54F, 0xE208, "VBL TIMINGPASSED")]
    [InlineData("vbl_nmi_timing/3.even_odd_frames.nes", 0xE59F, 0xE258, "EVEN ODD FRAMESPASSED")]
    [InlineData("vbl_nmi_timing/4.vbl_clear_timing.nes", 0xE535, 0xE1D7, "VBL CLEAR TIMINGPASSED")]
    [InlineData("vbl_nmi_timing/5.nmi_suppression.nes", 0xE54C, 0xE200, "NMI SUPPRESSIONPASSED")]
    [InlineData("vbl_nmi_timing/6.nmi_disable.nes", 0xE535, 0xE1DA, "NMI DISABLEPASSED")]
    [InlineData("vbl_nmi_timing/7.nmi_timing.nes", 0xE58E, 0xE247, "NMI TIMINGPASSED")]
    public void BeExecutedCorrectly(string romName, int powerOffAddress, int printAddress, string expectedResult)
    {
        var message = string.Empty;
        var ram = new byte[0x10000];
        var clock = CreateSetupWithRom(ram, romName, powerOffAddress, printAddress, (cpu, _) =>
            {
                message += (char)cpu.ReadByteFromAccumulator();
            });
        clock.Run();

        Assert.Equal(expectedResult, message);
    }
}