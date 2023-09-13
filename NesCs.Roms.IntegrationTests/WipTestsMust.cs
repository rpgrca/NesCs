using NesCs.Logic.Cpu;
using NesCs.Logic.Nmi;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;
using NesCs.Logic.Clocking;
using NesCs.Logic.Tracing;

namespace NesCs.Roms.IntegrationTests;

public class WipTestsMust
{
    [Theory]
    [InlineData("instr_timing/rom_singles/1-instr_timing.nes", 0x1, 2, "", Skip = "does nothing after Instruction timing test\n\nTakes about 25 seconds. Doesn't time the 8 branches and 12 illegal instructions.\n\n")]
    [InlineData("branch_timing_tests/1.Branch_Basics.nes", 0xE4F0, 1, "", Skip = "no output at all")]
    [InlineData("branch_timing_tests/2.Backward_Branch.nes", 0x1, 1, "", Skip = "no output at all")]
    [InlineData("branch_timing_tests/3.Forward_Branch.nes", 0x1, 1, "", Skip = "no output at all")]
    [InlineData("dmc_dma_during_read4/dma_2007_write.nes", 0x1, 2, "", Skip = "freezes at bit 0x10 / bne")]
    [InlineData("dmc_dma_during_read4/dma_2007_read.nes", 0x1, 2, "", Skip = "freezes at bit 0x10 / bne")]
    [InlineData("dmc_dma_during_read4/double_2007_read.nes", 0x1, 2, "", Skip = "freezes at sta 0, lda 0, jmp?")]
    [InlineData("sprdma_and_dmc_dma/sprdma_and_dmc_dma.nes", 0x1, 2, "", Skip = "must implement dma, shows only first line T+ Clocks (decimal)\n00 ")]
    [InlineData("vbl_nmi_timing/1.frame_basics.nes", 0x1, 1, "", Skip = "no output at all")]
    [InlineData("vbl_nmi_timing/2.vbl_timing.nes", 0x1, 1, "", Skip = "no output at all")]
    [InlineData("cpu_interrupts_v2/rom_singles/1-cli_latency.nes", 0x1, 2, "", Skip = "\nAPU should generate IRQ when $4017 = $00\n\n1-cli_latency\n\nFailed #3\n")]
    [InlineData("cpu_interrupts_v2/rom_singles/2-nmi_and_brk.nes", 0x1, 2, "", Skip = "NMI BRK 00\n27  36  00 \n27  36  00 \n26  36  00 \n26  36  00 \n26  36  00 \n26  36  00 \n26  36  00 \n27  36  00 \n27  36  00 \n27  36  00 \n\n78689450\n2-nmi_and_brk\n\nFailed\n")]
    [InlineData("cpu_interrupts_v2/rom_singles/3-nmi_and_irq.nes", 0x1, 2, "", Skip = "NMI BRK\n23  00 \n23  00 \n21  00 \n21  00 \n20  00 \n20  00 \n20  00 \n20  00 \n00  00 \n00  00 \n00  00 \n00  00 \n\nF4561CE0\n3-nmi_and_irq\n\nFailed\n")]
    [InlineData("cpu_interrupts_v2/rom_singles/4-irq_and_dma.nes", 0x1, 2, "", Skip = "53 +0\n53 +1\n53 +2\n53 +3\n53 +4\n53 +5\n53 +6\n53 +7\n53 +8\n53 +9\n53 +10\n53 +11\n53 +12\n53 +13\n...\n53 +524\n53 +525\n53 +526\n53 +527\n\nD927EAD0\n4-irq_and_dma\n\nFailed\n")]
    public void BeExecutedCorrectly(string romName, int poweroffAddress, int expectedRomSize, string expectedResult)
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

        nmiGenerator.AttachTo(cpu);
        cpu.PowerOn();
        cpu.Reset();
        cpu.Run();

        Assert.Equal(expectedRomSize, nesFile.ProgramRomSize);
        var result = GetString(ram);
        Assert.Equal(expectedResult, result);
        Assert.Equal(1, ram[0xF0]);
    }

    private static string GetString(byte[] ram) =>
        System.Text.Encoding.ASCII.GetString(ram[0x6004..].TakeWhile(p => !p.Equals(0)).ToArray());

    [Fact(Skip = "wip")]
    public void Test1()
    {
        var ram = new byte[0x10000];
        ram[0xC000] = 0xEE;
        ram[0xC001] = 0x06;
        ram[0xC002] = 0x20;
        var vram = PopulateVram();
        var ramController = new RamController.Builder().WithRamOf(ram).PreventRomRewriting().Build();
        var clock = new Clock(0);
        var ppu = new Ppu2C02.Builder().WithRamController(ramController).WithVram(vram).WithClock(clock).Build();
        ramController.RegisterHook(ppu);

        var trace = new List<(int, byte, string)>();
        var cpu = new Cpu6502.Builder()
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithXAs(0x00)
            .WithClock(clock)
            .WithProgramCounterAs(0xC000)
            .TracingWith(new TracerSpy(trace))
            .Build();

        ppu.Write(AddressRegister.AddressIndex, 0x25);
        ppu.Write(AddressRegister.AddressIndex, 0xFA);
        cpu.Step();
        var read1 = ppu.Read(DataPort.DataIndex);
        var read2 = ppu.Read(DataPort.DataIndex);

        Assert.Collection(trace,
            p1 => { Assert.Equal((0xC000, 0xEE, "read"), p1); },
            p2 => { Assert.Equal((0xC001, 0x06, "read"), p2); },
            p3 => { Assert.Equal((0xC002, 0x20, "read"), p3); },
            p4 => { Assert.Equal((0x2006, 0xFA, "read"), p4); },
            p5 => { Assert.Equal((0x2006, 0xFA, "write"), p5); },
            p6 => { Assert.Equal((0x2006, 0xFB, "write"), p6); }
        );

        Assert.Equal(0xA6, read1);
        Assert.Equal(0xA6, read2);
    }

    private byte[] PopulateVram()
    {
        var vram = new byte[0x4000];
        for (var index = 0; index < 0x4000; index++)
        {
            vram[index] = index switch
            {
                >= 0x2400 and <= 0x24FF => (byte)(index - 0x2400 + 0x40),
                >= 0x2500 and <= 0x25FF => (byte)(index - 0x2500 + 0x80),
                >= 0x2600 and <= 0x26FF => (byte)(index - 0x2600 + 0xC0),
                >= 0x2700 and <= 0x2BFF => (byte)(index - 0x2700 + 0x00),
                _ => 0
            };
        }

        return vram;
    }

/*
    [Theory]
    [InlineData("cpu_flag_concurrency/test_cpu_flag_concurrency.nes", 0x1, 2, "", Skip = "hangs after some information")]
    public void BeExecutedCorrectly1(string romName, int poweroffAddress, int expectedRomSize, string expectedResult)
    {
        var ram = new byte[0x10000];
        var fsp = new FileSystemProxy.Builder().Loading(new NesFileOptions
        {
            LoadHeader = true,
            LoadTrainer = true,
            LoadProgramRom = true,
            LoadCharacterRom = true
        }).Build();

        var nesFile = fsp.Load("../../../extra/" + romName);
        var clock = new Clock(0, 300_000_000);
        var ramController = new RamController.Builder().WithRamOf(ram).PreventRomRewriting().Build();
        var nmiGenerator = new NmiGenerator();
        var ppu = new Ppu2C02.Builder().WithRamController(ramController).WithClock(clock).WithNmiGenerator(nmiGenerator).Build();
        ramController.RegisterHook(ppu);
        ramController.AddHook(0xF0, (_, value) =>
        {
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

        nmiGenerator.AttachTo(cpu);
        cpu.PowerOn();
        cpu.Reset();
        cpu.Run();

        Assert.Equal(expectedRomSize, nesFile.ProgramRomSize);
        var result = GetString(ram);
        Assert.Equal(0, ram[0x6000]);
        Assert.Equal(expectedResult, result);
    }*/
}