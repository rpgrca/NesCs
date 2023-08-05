namespace NesCs.SystemTests.C64TestSuite;

public class Program
{
    public static int Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine($"Usage: program testfile");
            return 1;
        }

        Console.WriteLine($"Loading {args[0]}...");

        var bin = File.ReadAllBytes(args[0]);
        var start = bin[1] << 8 | bin[0];
        var cpu = new NesCs.Logic.Cpu.Cpu6502.Builder()
            .Running(bin[2..])
            .ProgramMappedAt(start)
            .WithProcessorStatusAs(NesCs.Logic.Cpu.ProcessorStatus.I)
            .WithProgramCounterAs(0x0801)
            .WithStackPointerAt(0xFD)
            .RamPatchedAs(new (int, byte)[]
            {
                (0xA003, 0x80), (0xFFFE, 0x48), (0xFFFF, 0xFF), (0x01FE, 0xFF), (0x01FF, 0x7F), // memory locations
                (0xFF48, 0x48), (0xFF49, 0x8A), (0xFF4A, 0x48), (0xFF4B, 0x98), (0xFF4C, 0x48),
                (0xFF4D, 0xBA), (0xFF4E, 0xBD), (0xFF4F, 0x04), (0xFF50, 0x01), (0xFF51, 0x29),
                (0xFF52, 0x10), (0xFF53, 0xF0), (0xFF54, 0x03), (0xFF55, 0x6C), (0xFF56, 0x16),
                (0xFF57, 0x03), (0xFF58, 0x6C), (0xFF59, 0x14), (0xFF5A, 0x03) // kernal IRQ handler
            })
            .TracingWith(new Vm6502DebuggerDisplay())
            .Build();

        cpu.Run();

        return 0;
    }
}