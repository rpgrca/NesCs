using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;

namespace NesCs.Roms.IntegrationTests;

public class CpuResetMust
{
    [Theory]
    [InlineData("../../../../../nes-test-roms/cpu_reset/ram_after_reset.nes", "\n\nPress reset AFTER this message\ndisappears\n\n\n\nram_after_reset\n\nPassed\n")]
    [InlineData("../../../../../nes-test-roms/cpu_reset/registers.nes", "A  X  Y  P  S\n00 00 00 34 FD \n\n\nPress reset AFTER this message\ndisappears\n\n\nA  X  Y  P  S\n34 56 78 FF 0F \n\nregisters\n\nPassed\n")]
    public void ReturnPassed(string romName, string expectedResult)
    {
        const int PrintAddress = 0xE7E9;
        const int ResetAddress = 0xE29C;
        const int PowerOffAddress = 0xE9D5;

        var message = string.Empty;
        var fsp = new FileSystemProxy.Builder().Loading(new NesFileOptions
        {
            LoadHeader = true,
            LoadTrainer = true,
            LoadProgramRom = true,
            LoadCharacterRom = true
        }).Build();

        var nesFile = fsp.Load(romName);
        var ramController = new RamController.Builder().Build();
        var ppu = new Ppu2C02(ramController);

        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
        var cpu = builder
            .Running(nesFile.ProgramRom)
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithCallback(PrintAddress, cpu => message += (char)cpu.ReadByteFromAccumulator())
            .WithCallback(ResetAddress, cpu => cpu.Reset())
            .WithCallback(PowerOffAddress, cpu => cpu.Stop())
            .Build();

        cpu.PowerOn();
        cpu.Run();

        Assert.Equal(expectedResult, message);
    }
}