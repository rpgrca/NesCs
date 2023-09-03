using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;
using NesCs.Logic.Cpu.Clocking;
using NesCs.Logic.Tracing;

namespace NesCs.Roms.IntegrationTests;

public class PpuVblNmiMust
{
    [Theory]
    [InlineData("ppu_vbl_nmi/rom_singles/01-vbl_basics.nes", 0x1, "", Skip = "error 4, can't pass")] 
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
        ramController.AddHook(0x6000, (a, v) => {
            System.Diagnostics.Debugger.Break();
        });

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

        var result = GetString(ram);
        Assert.Equal(0, ram[0x6000]);
        Assert.Equal(expectedResult, result);
    }

    private static string GetString(byte[] ram) =>
        System.Text.Encoding.ASCII.GetString(ram[0x6004..].TakeWhile(p => !p.Equals(0)).ToArray());
}