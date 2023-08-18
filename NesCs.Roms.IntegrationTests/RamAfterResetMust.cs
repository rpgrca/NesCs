using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;

namespace NesCs.Roms.IntegrationTests;

public class RamAfterResetMust
{
    [Fact]
    public void ReturnPassed()
    {
        var message = string.Empty;
        var fsp = new FileSystemProxy.Builder().Loading(new NesFileOptions
        {
            LoadHeader = true,
            LoadTrainer = true,
            LoadProgramRom = true,
            LoadCharacterRom = true
        }).Build();

        var nesFile = fsp.Load("../../../../../nes-test-roms/cpu_reset/ram_after_reset.nes");
        var ramController = new RamController.Builder().Build();
        var ppu = new Ppu2C02(ramController);

        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
        var cpu = builder
            .Running(nesFile.ProgramRom)
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithCallback(0xE7E9, cpu => {
                var c = (char)cpu.ReadByteFromAccumulator();
                message += c;
            })
            .WithCallback(0xE29C, cpu => {
                cpu.Reset();
            })
            .WithCallback(0xE9D5, cpu => {
                cpu.Stop();
            })
            .Build();

        cpu.PowerOn();
        cpu.Run();

        Assert.Equal("\n\nPress reset AFTER this message\ndisappears\n\n\n\nram_after_reset\n\nPassed\n", message);
    }
}