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
    //[InlineData("ppu_vbl_nmi/rom_singles/01-vbl_basics.nes", 0xE8D5, "\n01-vbl_basics\n\nPassed\n")]
    //[InlineData("ppu_vbl_nmi/rom_singles/02-vbl_set_time.nes", 0xE8D5, "T+ 1 2\n00 - V\n01 - V\n02 - V\n03 - V\n04 - -\n05 V -\n06 V -\n07 V -\n08 V -\n\n02-vbl_set_time\n\nPassed\n")]
    //[InlineData("ppu_vbl_nmi/rom_singles/03-vbl_clear_time.nes", 0xE8D5, "00 V\n01 V\n02 V\n03 V\n04 V\n05 V\n06 -\n07 -\n08 -\n\n03-vbl_clear_time\n\nPassed\n")]
    //[InlineData("ppu_vbl_nmi/rom_singles/04-nmi_control.nes", 0xE8D5, "\n04-nmi_control\n\nPassed\n")]
    //[InlineData("ppu_vbl_nmi/rom_singles/05-nmi_timing.nes", 0xE8D5, "00 4\n01 4\n02 4\n03 3\n04 3\n05 3\n06 3\n07 3\n08 3\n09 2\n\n05-nmi_timing\n\nPassed\n")]
    //[InlineData("ppu_vbl_nmi/rom_singles/06-suppression.nes", 0xE8D5, "00 - N\n01 - N\n02 - N\n03 - N\n04 - -\n05 V -\n06 V -\n07 V N\n08 V N\n09 V N\n\n06-suppression\n\nPassed\n")]
    [InlineData("ppu_vbl_nmi/rom_singles/07-nmi_on_timing.nes", 0xE8D5, "00 N\n01 N\n02 N\n03 N\n04 N\n05 -\n06 -\n07 -\n08 -\n\n07-nmi_on_timing\n\nPassed\n")]
    //[InlineData("ppu_vbl_nmi/rom_singles/08-nmi_off_timing.nes", 0xE8D5, "03 -\n04 -\n05 -\n06 -\n07 N\n08 N\n09 N\n0A N\n0B N\n0C N\n\n08-nmi_off_timing\n\nPassed\n")]
    //[InlineData("ppu_vbl_nmi/rom_singles/09-even_odd_frames.nes", 0xE8D5, "00 01 01 02 \n09-even_odd_frames\n\nPassed\n")]
    //[InlineData("ppu_vbl_nmi/rom_singles/10-even_odd_timing.nes", 0xEAD5, "08 08 09 07 \n10-even_odd_timing\n\nPassed\n")]
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
        var rasterAddress = new RasterAddress();
        var nmiGenerator = new NmiGenerator(clock, rasterAddress);
        var ramController = new RamController.Builder().WithRamOf(ram).PreventRomRewriting().Build();
        var ppu = new Ppu2C02.Builder().WithRamController(ramController).WithClock(clock).WithNmiGenerator(nmiGenerator).WithRaster(rasterAddress).Build();
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