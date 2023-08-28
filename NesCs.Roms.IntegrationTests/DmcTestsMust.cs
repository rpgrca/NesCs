using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;

namespace NesCs.Roms.IntegrationTests;

public class DmcTestsMust
{
    [Theory]
    [InlineData("dmc_tests/buffer_retained.nes", 0xE149, "")]
    public void BeExecutedCorrectly(string romName, int poweroffAddress, string expectedResult)
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
            .WithCallback(poweroffAddress, cpu => cpu.Stop())
            .TracingWith(new Vm6502DebuggerDisplay())
            .Build();

        cpu.PowerOn();
        cpu.Run();

        // According to fceux
        var snapshot = cpu.TakeSnapshot();
        Assert.Equal(0xE149, snapshot.PC);
        Assert.Equal(0x09, snapshot.A);
        Assert.Equal(0xFF, snapshot.X);
        Assert.Equal(0x00, snapshot.Y);
        Assert.Equal(0xFF, snapshot.S);
        //Assert.Equal(ProcessorStatus.I | ProcessorStatus.C, snapshot.P); CI vs CIBX?
    }

    private static string GetString(byte[] ram) =>
        System.Text.Encoding.ASCII.GetString(ram[0x6004..].TakeWhile(p => !p.Equals(0)).ToArray());
}