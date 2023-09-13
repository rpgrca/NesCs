using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Nmi;
using NesCs.Logic.Ram;
using NesCs.Logic.File;
using NesCs.Logic.Clocking;
using NesCs.Logic.Tracing;

namespace NesCs.Roms.IntegrationTests;

public class BlarggPpuTestsMust
{
    [Theory]
    [InlineData("blargg_ppu_tests_2005.09.15b/vbl_clear_time.nes", 0xE3B3)]
    [InlineData("blargg_ppu_tests_2005.09.15b/palette_ram.nes", 0xE412)]
    [InlineData("blargg_ppu_tests_2005.09.15b/vram_access.nes", 0xE48D, Skip = "fails at #6")]
    [InlineData("blargg_ppu_tests_2005.09.15b/sprite_ram.nes", 0x1, Skip = "fails at #6")]
    [InlineData("blargg_ppu_tests_2005.09.15b/power_up_palette.nes", 0x1, Skip = "fails at #2")]
    public void BeExecutedCorrectly(string romName, int poweroffAddress)
    {
        var ram = new byte[0x10000];
        var fsp = new FileSystemProxy.Builder().Loading(new NesFileOptions
        {
            LoadHeader = true,
            LoadTrainer = true,
            LoadProgramRom = true,
            LoadCharacterRom = true
        }).Build();

        var nesFile = fsp.Load("../../../../../nes-test-roms/" + romName);
        var clock = new Clock(0);
        var ramController = new RamController.Builder().WithRamOf(ram).PreventRomRewriting().Build();
        var nmiGenerator = new NmiGenerator();
        var ppu = new Ppu2C02.Builder().WithRamController(ramController).WithClock(clock).WithNmiGenerator(nmiGenerator).Build();
        ramController.RegisterHook(ppu);
        ramController.AddHook(0xF0, (_, value) =>
        {
            System.Diagnostics.Debugger.Break();
        });

        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
        builder.ProgramMappedAt(0xC000);

        var cpu = builder
            .Running(nesFile.ProgramRom)
            .WithClock(clock)
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithCallback(poweroffAddress, (cpu, _) => cpu.Stop())
            .TracingWith(new Vm6502DebuggerDisplay())
            .Build();

        nmiGenerator.AttachTo(cpu);
        cpu.PowerOn();
        cpu.Reset();
        cpu.Run();

        Assert.Equal(1, ram[0xF0]);
    }
}