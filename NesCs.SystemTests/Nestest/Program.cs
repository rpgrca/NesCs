if (args.Length < 1)
{
    Console.WriteLine($"Usage: program path/to/nestest.nes");
    return 1;
}

Console.WriteLine($"Loading {args[0]}...");

var fsp = new FileSystemProxy.Builder().Loading(new NesFileOptions { LoadHeader = true, LoadTrainer = true, LoadProgramRom = true }).Build();
var nesFile = fsp.Load(args[0]);

Console.WriteLine($"Loaded!");
Console.WriteLine(nesFile.ToString());

var builder = new NesCs.Logic.Cpu.Cpu6502.Builder()
    .ProgramMappedAt(0x8000); // NROM-256 or NROM-128

if (nesFile.ProgramRomSize == 1)
{
    builder.ProgramMappedAt(0xC000); // NROM-128
}

var cpu = builder
    .Running(nesFile.ProgramRom)
    .SupportingInvalidInstructions()
    .WithCyclesAs(6)
    .WithProcessorStatusAs(NesCs.Logic.Cpu.ProcessorStatus.X | NesCs.Logic.Cpu.ProcessorStatus.I)
    .WithStackPointerAt(0xFD)
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
    .Build();

cpu.PowerOn();
cpu.Reset();
cpu.Run();

return 0;