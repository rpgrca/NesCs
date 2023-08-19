using NesCs.Logic.Cpu;
using NesCs.Logic.Ppu;
using NesCs.Logic.Ram;

if (args.Length < 1)
{
    Console.WriteLine($"Usage: program path/to/nestest.nes");
    return 1;
}

Console.WriteLine($"Loading {args[0]}...");

var fsp = new FileSystemProxy.Builder().Loading(new NesFileOptions
    {
        LoadHeader = true,
        LoadTrainer = true,
        LoadProgramRom = true,
        LoadCharacterRom = true
    }).Build();

var nesFile = fsp.Load(args[0]);

Console.WriteLine($"Loaded!");
Console.WriteLine(nesFile.ToString());

var memory = new byte[0x10000];
var ramController = new RamController.Builder().WithRamOf(memory).Build();
var ppu = new Ppu2C02.Builder().WithRamController(ramController).Build();

var builder = new NesCs.Logic.Cpu.Cpu6502.Builder()
    .ProgramMappedAt(0x8000); // NROM-256 or NROM-128


if (nesFile.ProgramRomSize == 1)
{
    builder.ProgramMappedAt(0xC000); // NROM-128
}

        const int PrintAddress = 0xE463; // 0xE558;
        const int PowerOffAddress = 0xE7B5; // 0xE7E9;
        var message = string.Empty;

var cpu = builder
    .Running(nesFile.ProgramRom)
    .SupportingInvalidInstructions()
    .WithProgramCounterAs(0xC000)
    .WithCyclesAs(6)
    .WithProcessorStatusAs(ProcessorStatus.X | ProcessorStatus.I)
    .WithStackPointerAt(0xFD)
    .WithRamController(ramController)
    .TracingWith(new Vm6502DebuggerDisplay())
    .WithCallback(0xC66E, cpu => {
        var h2 = cpu.PeekMemory(0x02);
        var h3 = cpu.PeekMemory(0x03);

        var (P, A, PC, X, Y, S) = cpu.TakeSnapshot();

        if (A == 0 && X == 0xFF && Y == 0x15 && S == 0xFD && (byte)P == 0x27 && h2 == 0 && h3 == 0)
        {
            Console.WriteLine("\nReached end successfully");
        }
        else
        {
            Console.WriteLine($"\nFailed! $02: {h2}, $03: {h3}");
        }
        cpu.Stop();
    })
    .WithCallback(PrintAddress, cpu => {
        var c = (char)cpu.ReadByteFromAccumulator();
        Console.Write($"{c}");
        System.Diagnostics.Debug.Print($"{c}");
        message += c;
    })
    .Build();

    var lowByte = 0;
    var highByte = 0;
    var firstWrite = true;

ramController.AddHook(0x2006, (a, v) => {
    if (firstWrite)
    {
        lowByte = v;
        firstWrite = false;
    }
    else
    {
        highByte = v;
        firstWrite = true;
    }

    message += (char)v;
});

cpu.PowerOn();
cpu.Run();

return 0;


class RamHook : IRamHook
{
    public Cpu6502 Cpu { get; set; }

    public void Call(int index, byte value, byte[] ram)
    {
        throw new NotImplementedException();
    }

    public bool CanHandle(int index)
    {
        throw new NotImplementedException();
    }
}