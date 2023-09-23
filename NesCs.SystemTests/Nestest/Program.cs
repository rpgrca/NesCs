using NesCs.Logic.Cpu;
using NesCs.Logic.Clocking;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;
using NesCs.Logic.Tracing;
using NesCs.Logic.Nmi;

if (args.Length < 1)
{
    Console.WriteLine($"Usage: program path/to/nestest.nes");
    return 1;
}

Console.WriteLine($"Loading {args[0]}...");

var fsp = new FileSystemProxy.Builder()
    .Loading(new NesFileOptions
    {
        LoadHeader = true,
        LoadTrainer = true,
        LoadProgramRom = true,
        LoadCharacterRom = true
    }).Build();

var nesFile = fsp.Load(args[0]);

Console.WriteLine($"Loaded!");
Console.WriteLine(nesFile.ToString());

var clock = new Clock(0);
var rasterAddress = new RasterAddress();
var nmiGenerator = new NmiGenerator(clock, rasterAddress);
var ramController = new RamController.Builder().Build();
var ppu = new Ppu2C02.Builder()
    .WithRamController(ramController)
    .WithClock(clock)
    .WithNmiGenerator(nmiGenerator)
    .WithRaster(rasterAddress)
    .WithClockDivisorOf(1)
    .Build();
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
    .WithProgramCounterAs(0xC000) // 0xC000 -> nesttest batch, 0xC004 normal game
    //.WithProcessorStatusAs(ProcessorStatus.X | ProcessorStatus.I) -> Not for nestest
    .TracingWith(new Vm6502DebuggerDisplay())
    .WithClockDivisorOf(3)
    .WithCallback(0xC66E, (cpu, _) =>
    cpu.Stop()
    )
    .Build();

nmiGenerator.AttachTo(cpu);
cpu.PowerOn();
cpu.Reset();
clock.Run();

var (P, A, PC, X, Y, S, CY) = cpu.TakeSnapshot();
var h2 = cpu.PeekMemory(0x02);
var h3 = cpu.PeekMemory(0x03);

if (A == 0 && X == 0xFF && Y == 0x15 && S == 0xFD && (byte)P == 0x27 && h2 == 0 && h3 == 0 && CY == 26554)
{
    Console.WriteLine("\nCPU executed correctly");
}

if (rasterAddress.Y == 233 && rasterAddress.X == 209)
{
    Console.WriteLine("\nPPU executed correctly");
}

return 0;