using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;

namespace NesCs.Roms.IntegrationTests;

public class CpuResetMust
{
    [Theory]
    [InlineData("cpu_reset/ram_after_reset.nes", 0xE29C, 0xE9D5, "\nram_after_reset\n\nPassed\n")] // working
    [InlineData("cpu_reset/registers.nes", 0xE29C, 0xE9D5, "A  X  Y  P  S\n34 56 78 FF 0F \n\nregisters\n\nPassed\n")] // working
    [InlineData("instr_misc/rom_singles/01-abs_x_wrap.nes", 0x0001, 0xE7B5, "\n01-abs_x_wrap\n\nPassed\n")] // working
    //[InlineData("instr_misc/rom_singles/02-branch_wrap.nes", 0x0001, 0xE7B5, "\n02-branch_wrap\n\nPassed\n")]
    //[InlineData("instr_misc/rom_singles/03-dummy_reads.nes", 0x0001, 0x0002, "")] // Not working
    [InlineData("instr_test-v3/rom_singles/01-implied.nes", 0x0001, 0xE976, "\n01-implied\n\nPassed\n")]
    //[InlineData("instr_test-v3/rom_singles/02-immediate.nes", 0x0001, 0xE976, "\n01-implied\n\nPassed\n")] // Not working, must implement 0x0B*/
    [InlineData("instr_test-v3/rom_singles/03-zero_page.nes", 0x0001, 0xE976, "\n03-zero_page\n\nPassed\n")]
    [InlineData("instr_test-v3/rom_singles/04-zp_xy.nes", 0x0001, 0xE976, "\n04-zp_xy\n\nPassed\n")]
    public void ReturnPassed(string romName, int resetAddress, int poweroffAddress, string expectedResult)
    {
        var message = string.Empty;
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
        var ppu = new Ppu2C02(ramController);

        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
        var cpu = builder
            .Running(nesFile.ProgramRom)
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithCallback(resetAddress, cpu => cpu.Reset())
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
/*
    // TODO: No message being displayed?
    [Theory]
    [InlineData("instr_misc/rom_singles/03-dummy_reads.nes", 0xE7AB)]
    public void ReturnZeroInAccumulator_WhenTestIsSuccessful(string romName, int poweroffAddress)
    {
        var message = string.Empty;
        const int PrintAddress = 0xE463; // 0xE558
        var fsp = new FileSystemProxy.Builder().Loading(new NesFileOptions
        {
            LoadHeader = true,
            LoadTrainer = true,
            LoadProgramRom = true,
            LoadCharacterRom = true
        }).Build();

        var nesFile = fsp.Load("../../../../../nes-test-roms/" + romName);
        var ramController = new RamController.Builder().Build();
        var ppu = new Ppu2C02(ramController);

        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
        var cpu = builder
            .Running(nesFile.ProgramRom)
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithCallback(PrintAddress, cpu => message += (char)cpu.ReadByteFromAccumulator())
            .Build();

        cpu.PowerOn();
        cpu.Run();

        Assert.Equal(0, cpu.PeekMemory(0x6000));
        Assert.Equal(0, cpu.ReadByteFromAccumulator());
    }*/
}