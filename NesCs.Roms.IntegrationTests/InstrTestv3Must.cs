using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;

namespace NesCs.Roms.IntegrationTests;

public class InstrTestv3Must
{
    [Theory]
    [InlineData("instr_test-v3/rom_singles/01-implied.nes", 0xE976, "\n01-implied\n\nPassed\n")] // ticks 43722073
    [InlineData("instr_test-v3/rom_singles/02-immediate.nes", 0xE976, "\n01-implied\n\nPassed\n", Skip = "Must implement 0x0B")]
    [InlineData("instr_test-v3/rom_singles/03-zero_page.nes", 0xE976, "\n03-zero_page\n\nPassed\n")] // ticks 50752549
    [InlineData("instr_test-v3/rom_singles/04-zp_xy.nes", 0xE544, "\n04-zp_xy\n\nPassed\n")] // ticks 89893861
    [InlineData("instr_test-v3/rom_singles/05-absolute.nes", 0xE976, "\n05-absolute\n\nPassed\n")] // ticks 48594541
    [InlineData("instr_test-v3/rom_singles/06-abs_xy.nes", 0xE976, "", Skip = "Must implement 0x9C or crash?")]
    [InlineData("instr_test-v3/rom_singles/07-ind_x.nes", 0xE976, "\n07-ind_x\n\nPassed\n")] // ticks 61211029
    [InlineData("instr_test-v3/rom_singles/08-ind_y.nes", 0xE876, "\n08-ind_y\n\nPassed\n")] // ticks 58132009
    [InlineData("instr_test-v3/rom_singles/09-branches.nes", 0xE876, "\n09-branches\n\nPassed\n")] // ticks 23867617
    [InlineData("instr_test-v3/rom_singles/10-stack.nes", 0xE876, "\n10-stack\n\nPassed\n")] // ticks 67658665
    [InlineData("instr_test-v3/rom_singles/11-jmp_jsr.nes", 0xE876, "\n11-jmp_jsr\n\nPassed\n")] // ticks 15339457
    [InlineData("instr_test-v3/rom_singles/12-rts.nes", 0xE876, "\n12-rts\n\nPassed\n")] // ticks 14148877
    [InlineData("instr_test-v3/rom_singles/13-rti.nes", 0xE876, "\n13-rti\n\nPassed\n")] // ticks 14166121
    [InlineData("instr_test-v3/rom_singles/14-brk.nes", 0xE876, "\n14-brk\n\nPassed\n")] // ticks 17952733
    [InlineData("instr_test-v3/rom_singles/15-special.nes", 0xE8D5, "\n15-special\n\nPassed\n")] // ticks 13356193
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
            .WithRamController(ramController)
            .WithClock(clock)
            .WithCallback(poweroffAddress, cpu => cpu.Stop())
            .Build();

        cpu.PowerOn();
        cpu.Run();

        var result = GetString(ram);
        Assert.Equal(0, ram[0x6000]);
        Assert.Equal(expectedResult, result);
    }

    private static string GetString(byte[] ram) =>
        System.Text.Encoding.ASCII.GetString(ram[0x6004..].TakeWhile(p => !p.Equals(0)).ToArray());
}