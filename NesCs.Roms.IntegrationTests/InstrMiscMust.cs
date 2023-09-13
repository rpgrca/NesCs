using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;
using NesCs.Logic.Cpu.Clocking;

namespace NesCs.Roms.IntegrationTests;

public class InstrMiscMust
{
    [Theory]
    [InlineData("instr_misc/rom_singles/01-abs_x_wrap.nes", 0xE7B5, 1, "\n01-abs_x_wrap\n\nPassed\n")] // ticks: 786229
    [InlineData("instr_misc/rom_singles/02-branch_wrap.nes", 0xE7B5, 1, "\n02-branch_wrap\n\nPassed\n")] // ticks: 793369
    [InlineData("instr_misc/rom_singles/03-dummy_reads.nes", 0x1, 12, "", Skip = "\nSTA abs,x\n\n03-dummy_reads\n\nFailed #4\n")]
    public void ReturnPassed(string romName, int poweroffAddress, int clockDivisor, string expectedResult)
    {
        var clock = new Clock(0);
        var ram = new byte[0x10000];
        var fsp = new FileSystemProxy.Builder().Loading(new NesFileOptions
        {
            LoadHeader = true,
            LoadTrainer = true,
            LoadProgramRom = true,
            LoadCharacterRom = true
        }).Build();

        var nesFile = fsp.Load("../../../../../nes-test-roms/" + romName);
        var ramController = new RamController.Builder().WithRamOf(ram).Build();
        var ppu = new Ppu2C02.Builder().WithRamController(ramController).WithClock(clock).Build();
        ramController.RegisterHook(ppu);

        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
        var cpu = builder
            .Running(nesFile.ProgramRom)
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithClock(clock)
            .WithClockDivisorOf(clockDivisor)
            .WithCallback(poweroffAddress, (cpu, _) => cpu.Stop())
            .Build();

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