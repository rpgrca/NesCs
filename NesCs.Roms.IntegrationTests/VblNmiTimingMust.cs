using NesCs.Logic.Cpu;
using NesCs.Logic.Nmi;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;
using NesCs.Logic.Clocking;
using NesCs.Logic.Tracing;

namespace NesCs.Roms.IntegrationTests;

public class VblNmiTimingMust
{
    [Theory]
    [InlineData("vbl_nmi_timing/1.frame_basics.nes", 0xE589, 0xE217, "", Skip = "PPU FRAME BASICSFAILED: #\06")]
    [InlineData("vbl_nmi_timing/2.vbl_timing.nes", 0xE54F, 0xE208, "", Skip = "VBL TIMINGFAILED #8")]
    [InlineData("vbl_nmi_timing/3.even_odd_frames.nes", 0xE59F, 0xE258, "", Skip = "EVEN ODD FRAMESFAILED #2")]
    [InlineData("vbl_nmi_timing/4.vbl_clear_timing.nes", 0xE535, 0xE1D7, "", Skip = "VBL CLEAR TIMINGFAILED #3")]
    [InlineData("vbl_nmi_timing/5.nmi_suppression.nes", 0xE54C, 0xE200, "", Skip = "NMI SUPPRESSIONFAILED #3")]
    [InlineData("vbl_nmi_timing/6.nmi_disable.nes", 0xE535, 0xE1DA, "", Skip = "NMI DISABLEFAILED #2")]
    [InlineData("vbl_nmi_timing/7.nmi_timing.nes", 0xE58E, 0xE247, "", Skip = "NMI TIMINGFAILED #5")]
    public void BeExecutedCorrectly(string romName, int poweroffAddress, int printAddress, string expectedResult)
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
        var rasterAddress = new RasterAddress();
        var nmiGenerator = new NmiGenerator(clock, rasterAddress);
        var ppu = new Ppu2C02.Builder().WithRamController(ramController).WithClock(clock).WithNmiGenerator(nmiGenerator).WithRaster(rasterAddress).Build();
        ramController.RegisterHook(ppu);
        ramController.AddHook(0xF8, (i, b) => {
            //System.Diagnostics.Debugger.Break();
        });

        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
        builder.ProgramMappedAt(0xC000);

        var message = string.Empty;
        var cpu = builder
            .Running(nesFile.ProgramRom)
            .WithClock(clock)
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithCallback(poweroffAddress, (cpu, _) => cpu.Stop())
            .WithCallback(printAddress, (cpu, _) =>
            {
                message += (char)cpu.ReadByteFromAccumulator();
            })
            .TracingWith(new Vm6502DebuggerDisplay())
            .Build();

        nmiGenerator.AttachTo(cpu);
        cpu.PowerOn();
        cpu.Reset();
        cpu.Run();

        Assert.Equal(expectedResult, message);
    }
}