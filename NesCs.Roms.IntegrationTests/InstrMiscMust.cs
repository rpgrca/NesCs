using static NesCs.Roms.IntegrationTests.Utilities;

namespace NesCs.Roms.IntegrationTests;

public class InstrMiscMust
{
    [Theory]
    [InlineData("instr_misc/rom_singles/01-abs_x_wrap.nes", 0xE7B5, "\n01-abs_x_wrap\n\nPassed\n")] // ticks: 786229
    [InlineData("instr_misc/rom_singles/02-branch_wrap.nes", 0xE7B5, "\n02-branch_wrap\n\nPassed\n")] // ticks: 793369
    [InlineData("instr_misc/rom_singles/03-dummy_reads.nes", 0x1, "", Skip ="\nROL abs,x\n\n03-dummy_reads\n\nFailed #10\n")]
    [InlineData("instr_misc/rom_singles/04-dummy_reads_apu.nes", 0x1, "", Skip = "1D 19 11 3D 39 31 5D 59 51 7D 79 71 9D 99 91 BD B9 B1 DD D9 D1 FD F9 F1 1E 3E 5E 7E DE FE BC BE \nOfficial opcodes failed\n\n04-dummy_reads_apu\n\nFailed #2\n")]
    public void ReturnPassed(string romName, int poweroffAddress, string expectedResult)
    {
        var ram = new byte[0x10000];
        var clock = CreateSetup(ram, romName, poweroffAddress);
        clock.Run();

        var result = GetString(ram);
        Assert.Equal(0, ram[0x6000]);
        Assert.Equal(expectedResult, result);
    }

    private static string GetString(byte[] ram) =>
        System.Text.Encoding.ASCII.GetString(ram[0x6004..].TakeWhile(p => !p.Equals(0)).ToArray());
}