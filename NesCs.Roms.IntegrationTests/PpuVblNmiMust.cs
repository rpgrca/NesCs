using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;
using NesCs.Logic.Clocking;
using NesCs.Logic.Tracing;
using NesCs.Logic.Nmi;

namespace NesCs.Roms.IntegrationTests;

public class PpuVblNmiMust
{
    [Theory]
    [InlineData("ppu_vbl_nmi/rom_singles/01-vbl_basics.nes", 0x1, "", Skip = "Error 1: T+ 1 2\n00 - V\n01 - V\n02 V -\n03 V -\n04 V -\n05 V -\n06 V -\n07 V -\n08 V -\n\nC2633058\n02-vbl_set_time\n\nFailed\n")]
    [InlineData("ppu_vbl_nmi/rom_singles/02-vbl_set_time.nes", 0x1, "", Skip = "Passes first two T+ 1 2\n00 - V\n01 - V\n02 V -\n03 V -\n04 V -\n05 V -\n06 V -\n07 V -\n08 V -\n\nC2633058\n02-vbl_set_time\n\nFailed\n")]
    [InlineData("ppu_vbl_nmi/rom_singles/03-vbl_clear_time.nes", 0x1, "", Skip = "Passes first two 00 V\n01 V\n02 -\n03 -\n04 -\n05 -\n06 -\n07 -\n08 -\n\nF40A7AF2\n03-vbl_clear_time\n\nFailed\n")]
    [InlineData("ppu_vbl_nmi/rom_singles/04-nmi_control.nes", 0x1, "\n04-nmi_control\n\nPassed\n")]
    [InlineData("ppu_vbl_nmi/rom_singles/05-nmi_timing.nes", 0x1, "", Skip = "00 2\n01 2\n02 2\n03 1\n04 1\n05 1\n06 1\n07 1\n08 1\n09 1\n\nDFCA3A72\n05-nmi_timing\n\nFailed\n")]
    [InlineData("ppu_vbl_nmi/rom_singles/06-suppression.nes", 0x1, "", Skip = "00 - N\n01 - N\n02 V N\n03 V N\n04 V N\n05 V N\n06 V N\n07 V N\n08 V N\n09 V N\n\n636EA6C0\n06-suppression\n\nFailed\n")]
    [InlineData("ppu_vbl_nmi/rom_singles/07-nmi_on_timing.nes", 0x1, "", Skip = "00 N\n01 N\n02 -\n03 -\n04 -\n05 -\n06 -\n07 -\n08 -\n\n8EA0FFD9\n07-nmi_on_timing\n\nFailed\n")]
    [InlineData("ppu_vbl_nmi/rom_singles/08-nmi_off_timing.nes", 0x1, "", Skip = "03 N\n04 N\n05 N\n06 N\n07 N\n08 N\n09 N\n0A N\n0B N\n0C N\n\nA46D9938\n08-nmi_off_timing\n\nFailed\n")]
    [InlineData("ppu_vbl_nmi/rom_singles/09-even_odd_frames.nes", 0x1, "", Skip = "04 \nPattern ----- should not skip any clocks\n\n09-even_odd_frames\n\nFailed #2\n")]
    [InlineData("ppu_vbl_nmi/rom_singles/10-even_odd_timing.nes", 0x1, "", Skip = "02 \nClock is skipped too soon, relative to enabling BG\n\n10-even_odd_timing\n\nFailed #2\n")]
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
        var nmiGenerator = new NmiGenerator();
        var ramController = new RamController.Builder().WithRamOf(ram).PreventRomRewriting().Build();
        var ppu = new Ppu2C02.Builder().WithRamController(ramController).WithClock(clock).WithNmiGenerator(nmiGenerator).Build();
        ramController.RegisterHook(ppu);

        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
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

        var result = GetString(ram);
        Assert.Equal(0, ram[0x6000]);
        Assert.Equal(expectedResult, result);
    }

    private static string GetString(byte[] ram) =>
        System.Text.Encoding.ASCII.GetString(ram[0x6004..].TakeWhile(p => !p.Equals(0)).ToArray());
}