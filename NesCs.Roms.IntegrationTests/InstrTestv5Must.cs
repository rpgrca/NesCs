using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;
using NesCs.Logic.Clocking;

namespace NesCs.Roms.IntegrationTests;

public class InstrTestv5Must
{
    [Theory]
    [InlineData("instr_test-v5/rom_singles/01-basics.nes", 0xE8D5, "\n01-basics\n\nPassed\n")] // ticks 16146661
    [InlineData("instr_test-v5/rom_singles/02-implied.nes", 0xE976, "\n02-implied\n\nPassed\n")] // ticks 44434513
    [InlineData("instr_test-v5/rom_singles/03-immediate.nes", 0xE8D5, "\n03-implied\n\nPassed\n", Skip = "Must implement 0x0B")]
    [InlineData("instr_test-v5/rom_singles/04-zero_page.nes", 0xE976, "\n04-zero_page\n\nPassed\n")] // ticks 51495505
    [InlineData("instr_test-v5/rom_singles/05-zp_xy.nes", 0xE546 /*0xE976*/, "\n05-zp_xy\n\nPassed\n")] // ticks 90701173
    [InlineData("instr_test-v5/rom_singles/06-absolute.nes", 0xE976, "\n06-absolute\n\nPassed\n")] // ticks 49311253
    [InlineData("instr_test-v5/rom_singles/07-abs_xy.nes", 0xE8D5, "", Skip = "Must implement 0x9C?")]
    [InlineData("instr_test-v5/rom_singles/08-ind_x.nes", 0xE976, "\n08-ind_x\n\nPassed\n")] // ticks 61959085
    [InlineData("instr_test-v5/rom_singles/09-ind_y.nes", 0xE976, "\n09-ind_y\n\nPassed\n")] // ticks 58842445
    [InlineData("instr_test-v5/rom_singles/10-branches.nes", 0xE876, "\n10-branches\n\nPassed\n")] // ticks 24585097
    [InlineData("instr_test-v5/rom_singles/11-stack.nes", 0xE976, "\n11-stack\n\nPassed\n")] // ticks 68377561
    [InlineData("instr_test-v5/rom_singles/12-jmp_jsr.nes", 0xE876, "\n12-jmp_jsr\n\nPassed\n")] // ticks 16056301
    [InlineData("instr_test-v5/rom_singles/13-rts.nes", 0xE876, "\n13-rts\n\nPassed\n")] // ticks 14861689
    [InlineData("instr_test-v5/rom_singles/14-rti.nes", 0xE876, "\n14-rti\n\nPassed\n")] // ticks 14885269
    [InlineData("instr_test-v5/rom_singles/15-brk.nes", 0xE876, "\n15-brk\n\nPassed\n")] // ticks 18668101
    [InlineData("instr_test-v5/rom_singles/16-special.nes", 0xE7D5, "\n16-special\n\nPassed\n")] // ticks 14077777
    public void ReturnPassed(string romName, int poweroffAddress, string expectedResult)
    {
        var clock = new Clock(0);
        var ram = new byte[0x10000];
        var fsp = new FileSystemProxy.Builder().Loading(new NesFileOptions
        {
            LoadHeader = true,
            LoadTrainer = true,
            LoadProgramRom = true,
            LoadCharacterRom = true
        }).Build();

        var nesFile = fsp.Load("../../../../../nes-test-roms/" + romName);
        var ramController = new RamController.Builder().WithRamOf(ram).Build();
        var ppu = new Ppu2C02.Builder().WithRamController(ramController).WithClock(clock).Build();

        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
        var cpu = builder
            .Running(nesFile.ProgramRom)
            .SupportingInvalidInstructions()
            .WithClock(clock)
            .WithClockDivisorOf(1)
            .WithRamController(ramController)
            .WithCallback(poweroffAddress, (cpu, _) => cpu.Stop())
            .Build();

        cpu.PowerOn();
        cpu.Reset();
        cpu.Run();

        var result = GetString(ram);
        Assert.Equal(2, nesFile.ProgramRomSize);
        Assert.Equal(0, ram[0x6000]);
        Assert.Equal(expectedResult, result);
    }

    private static string GetString(byte[] ram) =>
        System.Text.Encoding.ASCII.GetString(ram[0x6004..].TakeWhile(p => !p.Equals(0)).ToArray());
}