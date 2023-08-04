if (args.Length < 1)
{
    Console.WriteLine($"Usage: program <filename>");
    return 1;
}

Console.WriteLine($"Loading {args[0]}...");

var fsp = new FileSystemProxy.Builder().Loading(new NesFileOptions { LoadHeader = true, LoadTrainer = true, LoadProgramRom = true }).Build();
var nesFile = fsp.Load(args[0]);

Console.WriteLine($"Loaded!");
Console.WriteLine(nesFile.ToString());

var cpu = new NesCs.Logic.Cpu.Cpu6502.Builder()
    .Running(nesFile.ProgramRom)
    .ImageStartsAt(0xC000)
    .WithCyclesAs(7)
    .WithProgramCounterAs(0xC000)
    .WithProcessorStatusAs(NesCs.Logic.Cpu.ProcessorStatus.X | NesCs.Logic.Cpu.ProcessorStatus.I)
    .WithStackPointerAt(0xFD)
    .TracingWith(new Vm6502DebuggerDisplay())
    .Build();

cpu.Run();

return 0;