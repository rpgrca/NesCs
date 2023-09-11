using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.File;
using NesCs.Logic.Cpu.Clocking;

namespace NesCs.Roms.IntegrationTests;

public class CpuResetMust
{
    [Theory]
    [InlineData("cpu_reset/ram_after_reset.nes", 0xE29C, 0xE9D5, "\nram_after_reset\n\nPassed\n")] // ticks: 57819145
    [InlineData("cpu_reset/registers.nes", 0xE29C, 0xE9D5, "A  X  Y  P  S\n34 56 78 FF 0F \n\nregisters\n\nPassed\n")] // ticks: 57487249
    public void ReturnPassed(string romName, int resetAddress, int poweroffAddress, string expectedResult)
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

        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
        var cpu = builder
            .Running(nesFile.ProgramRom)
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithClock(clock)
            .WithClockDivisorOf(1)
            .WithCallback(resetAddress, (cpu, _) => cpu.Reset())
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
}