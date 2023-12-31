using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Nmi;
using NesCs.Logic.Ram;
using NesCs.Logic.File;
using NesCs.Logic.Clocking;
using NesCs.Logic.Tracing;

namespace NesCs.Roms.IntegrationTests;

public class PpuReadBufferMust
{
    // TODO: Error 59 was with Rom memory
    // 
    [Theory]
    [InlineData("ppu_read_buffer/test_ppu_read_buffer.nes", 0x1, "", Skip = "\u001b[0;37mTEST:test_ppu_read_buffer\n-----------------------------\nTesting basic PPU memory I/O.\n")]
    public void BeExecutedCorrectly(string romName, int powerOffAddress, string expectedResult)
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
        var rasterAddress = new RasterAddress();
        var nmiGenerator = new NmiGenerator(clock, rasterAddress);
        var ramController = new RamController.Builder()
            .WithRamOf(ram)
            //.PreventRomRewriting() makes error 59 appear
            .Build();
        var ppu = new Ppu2C02.Builder().WithRamController(ramController).WithClock(clock).WithNmiGenerator(nmiGenerator).WithRaster(rasterAddress).Build();
        ramController.RegisterHook(ppu);
        ramController.AddHook(0x6000, (i, b) => {
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
            .WithCallback(powerOffAddress, (cpu, _) => cpu.Stop())
            .TracingWith(new Vm6502DebuggerDisplay())
            .Build();

        nmiGenerator.AttachTo(cpu);
        cpu.PowerOn();
        cpu.Run();

        var result = GetString(ram);
        Assert.Equal(0, ram[0x6000]);
        Assert.Equal(expectedResult, result);
    }

    private static string GetString(byte[] ram) =>
        System.Text.Encoding.ASCII.GetString(ram[0x6004..].TakeWhile(p => !p.Equals(0)).ToArray());
}