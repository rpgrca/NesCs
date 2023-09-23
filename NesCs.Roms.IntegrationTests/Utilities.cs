using NesCs.Logic.Clocking;
using NesCs.Logic.Cpu;
using NesCs.Logic.File;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;

namespace NesCs.Roms.IntegrationTests;

public static class Utilities
{
    public static IClock CreateSetup(byte[] ram, string romName, int powerOffAddress)
    {
        var clock = new Clock(0);
        var fsp = new FileSystemProxy.Builder().Loading(new NesFileOptions
        {
            LoadHeader = true,
            LoadTrainer = true,
            LoadProgramRom = true,
            LoadCharacterRom = true
        }).Build();

        var nesFile = fsp.Load("../../../../../nes-test-roms/" + romName);
        var ramController = new RamController.Builder()
            .WithRamOf(ram)
            .Build();

        var ppu = new Ppu2C02.Builder()
            .WithRamController(ramController)
            .WithClock(clock)
            .WithClockDivisorOf(1)
            .Build();

        ramController.RegisterHook(ppu);
        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
        var cpu = builder
            .Running(nesFile.ProgramRom)
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithClock(clock)
            .WithClockDivisorOf(3)
            .WithCallback(powerOffAddress, (cpu, _) => cpu.Stop())
            .Build();

        cpu.PowerOn();
        cpu.Reset();

        return clock;
    }
}