using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;
using NesCs.Logic.Cpu.Clocking;

namespace NesCs.Roms.IntegrationTests;

public class CpuTimingTest6Must
{
    [Theory]
    [InlineData("cpu_timing_test6/cpu_timing_test.nes", 0xE976, "\n01-basics\n\nPassed\n", Skip = "must validate return")]
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
            builder.ProgramMappedAt(0xC000); // NROM-128
        }

        var cpu = builder
            .Running(nesFile.ProgramRom)
            .WithClock(clock)
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithCallback(poweroffAddress, (cpu, _) => cpu.Stop())
            .Build();

        cpu.PowerOn();
        cpu.Run();

        var result = GetString(ram);
        Assert.Equal(0, ram[0x6000]);
        Assert.Equal(expectedResult, result);
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
}