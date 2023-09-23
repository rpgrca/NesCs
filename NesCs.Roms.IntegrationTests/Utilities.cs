using NesCs.Logic.Clocking;
using NesCs.Logic.Cpu;
using NesCs.Logic.File;
using NesCs.Logic.Nmi;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;

namespace NesCs.Roms.IntegrationTests;

public static class Utilities
{
    private const int CpuDivisor = 3;
    private const int PpuDivisor = 1;

    private static INesFile LoadNesFile(string romName)
    {
        var fsp = new FileSystemProxy.Builder().Loading(new NesFileOptions
        {
            LoadHeader = true,
            LoadTrainer = true,
            LoadProgramRom = true,
            LoadCharacterRom = true
        }).Build();

        return fsp.Load("../../../../../nes-test-roms/" + romName);
    }

    private static IRamController CreateRamController(byte[] ram) =>
        new RamController.Builder()
            .WithRamOf(ram)
            .Build();

    private static IPpu CreatePpu(IClock clock, IRamController ramController, INmiGenerator nmiGenerator)
    {
        var ppu = new Ppu2C02.Builder()
            .WithRamController(ramController)
            .WithClock(clock)
            .WithClockDivisorOf(PpuDivisor)
            .WithNmiGenerator(nmiGenerator)
            .Build();

        ramController.RegisterHook(ppu);
        return ppu;
    }

    private static Cpu6502.Builder CreateCpu(IClock clock, INesFile nesFile, IRamController ramController)
    {
        var builder = new Cpu6502.Builder().ProgramMappedAt(0x8000);
        if (nesFile.ProgramRomSize == 1)
        {
            builder.ProgramMappedAt(0xC000); // NROM-128
        }

        return builder
            .Running(nesFile.ProgramRom)
            .SupportingInvalidInstructions()
            .WithRamController(ramController)
            .WithClock(clock)
            .WithClockDivisorOf(CpuDivisor);
    }

    private static Cpu6502 CreateCpuWith(IClock clock, INesFile nesFile, IRamController ramController, int powerOffAddress) =>
        CreateCpu(clock, nesFile, ramController)
            .WithCallback(powerOffAddress, (cpu, _) => cpu.Stop())
            .Build();

    private static Cpu6502 CreateCpuWith(IClock clock, INesFile nesFile, IRamController ramController, int poweroffAddress, int resetAddress) =>
        CreateCpu(clock, nesFile, ramController)
            .WithCallback(poweroffAddress, (cpu, _) => cpu.Stop())
            .WithCallback(resetAddress, (cpu, _) => cpu.Reset())
            .Build();

    public static IClock CreateSetup(byte[] ram, string romName, int powerOffAddress)
    {
        var clock = new Clock(0);
        var nesFile = LoadNesFile(romName);
        var ramController = CreateRamController(ram);
        var rasterAddress = new RasterAddress();
        var nmiGenerator = new NmiGenerator(clock, rasterAddress);
        var ppu = CreatePpu(clock, ramController, nmiGenerator);
        var cpu = CreateCpuWith(clock, nesFile, ramController, powerOffAddress);
        nmiGenerator.AttachTo(cpu);

        cpu.PowerOn();
        cpu.Reset();
        return clock;
    }

    public static IClock CreateSetup(byte[] ram, string romName, int powerOffAddress, int resetAddress)
    {
        var clock = new Clock(0);
        var nesFile = LoadNesFile(romName);
        var ramController = CreateRamController(ram);
        var rasterAddress = new RasterAddress();
        var nmiGenerator = new NmiGenerator(clock, rasterAddress);
        var ppu = CreatePpu(clock, ramController, nmiGenerator);
        var cpu = CreateCpuWith(clock, nesFile, ramController, powerOffAddress, resetAddress);
        nmiGenerator.AttachTo(cpu);

        cpu.PowerOn();
        cpu.Reset();
        return clock;
    }
}