using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;
using NesCs.Logic.Tracing;
using NesCs.Logic.Cpu.Clocking;

namespace NesCs.Roms.IntegrationTests;

public class DmcTestsMust
{
    [Theory]
    [InlineData("dmc_tests/buffer_retained.nes", 0xE149, 0x09, 0xFF, 0x00, 0xFF)]
    [InlineData("dmc_tests/latency.nes", 0xE162, 0x09, 0xFF, 0x00, 0xFF)]
    [InlineData("dmc_tests/status.nes", 0xE12A, 0x09, 0xFF, 0x00, 0xFF, Skip = "0xE14E expected")]
    [InlineData("dmc_tests/status_irq.nes", 0xE154, 0x09, 0xFF, 0x00, 0xFF)]
    public void BeExecutedCorrectly(string romName, int poweroffAddress, int a, int x, int y, int s)
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
        var ppu = new Ppu2C02.Builder().WithRamController(ramController).WithClock(clock).Build();
        ramController.RegisterHook(ppu);

        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
        if (nesFile.ProgramRomSize == 1)
        {
            builder.ProgramMappedAt(0xC000);
        }

        var cpu = builder
            .Running(nesFile.ProgramRom)
            .WithClock(clock)
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithCallback(poweroffAddress, (cpu, _) => cpu.Stop())
            .TracingWith(new Vm6502DebuggerDisplay())
            .Build();

        cpu.PowerOn();
        cpu.Run();

        // According to fceux
        var snapshot = cpu.TakeSnapshot();
        Assert.Equal(poweroffAddress, snapshot.PC);
        Assert.Equal(a, snapshot.A);
        Assert.Equal(x, snapshot.X);
        Assert.Equal(y, snapshot.Y);
        Assert.Equal(s, snapshot.S);
        //Assert.Equal(ProcessorStatus.I | ProcessorStatus.C, snapshot.P); CI vs CIBX?
    }

    private static string GetString(byte[] ram) =>
        System.Text.Encoding.ASCII.GetString(ram[0x6004..].TakeWhile(p => !p.Equals(0)).ToArray());
}