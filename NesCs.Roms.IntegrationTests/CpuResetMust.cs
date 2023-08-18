using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;

namespace NesCs.Roms.IntegrationTests;

public class CpuResetMust
{
    [Theory]
    [InlineData("cpu_reset/ram_after_reset.nes", 0xE7E9, 0xE29C, 0xE9D5, "\n\nPress reset AFTER this message\ndisappears\n\n\n\nram_after_reset\n\nPassed\n")]
    [InlineData("cpu_reset/registers.nes", 0xE7E9, 0xE29C, 0xE9D5, "A  X  Y  P  S\n00 00 00 34 FD \n\n\nPress reset AFTER this message\ndisappears\n\n\nA  X  Y  P  S\n34 56 78 FF 0F \n\nregisters\n\nPassed\n")]
    [InlineData("instr_misc/rom_singles/01-abs_x_wrap.nes", 0xE463, 0x0000, 0xE7B5, "\n01-abs_x_wrap\n\nPassed\n")]
    public void ReturnPassed(string romName, int printAddress, int resetAddress, int poweroffAddress, string expectedResult)
    {
        var message = string.Empty;
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
            .WithCallback(printAddress, cpu => message += (char)cpu.ReadByteFromAccumulator())
            .WithCallback(resetAddress, cpu => cpu.Reset())
            .WithCallback(poweroffAddress, cpu => cpu.Stop())
            .Build();

        cpu.PowerOn();
        cpu.Run();

        Assert.Equal(expectedResult, message);
    }
}