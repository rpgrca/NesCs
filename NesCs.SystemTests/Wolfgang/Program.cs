namespace NesCs.SystemTests.C64TestSuite;

// http://www.softwolves.com/arkiv/cbm-hackers/7/7114.html
public class Program
{
    public static int Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine($"Usage: program testfile");
            return 1;
        }

        if (! File.Exists(args[0]))
        {
            Console.WriteLine($"File {args[0]} does not exist. Aborting.");
            return 1;
        }

        Console.WriteLine($"Trying to load {args[0]}...");
        var directory = new FileInfo(args[0]).Directory!.FullName;
        var bin = File.ReadAllBytes(args[0]);
        var start = bin[1] << 8 | bin[0];
        var cpu = new NesCs.Logic.Cpu.Cpu6502.Builder()
            .Running(bin[2..])
            .ProgramMappedAt(start)
            .WithProcessorStatusAs(NesCs.Logic.Cpu.ProcessorStatus.I)
            .WithProgramCounterAs(0x0801)
            .WithStackPointerAt(0xFD)
            .SupportingInvalidInstructions()
            .RamPatchedAs(new (int, byte)[]
            {
                (0xA003, 0x80), (0xFFFE, 0x48), (0xFFFF, 0xFF), (0x01FE, 0xFF), (0x01FF, 0x7F), // memory locations
                (0xFF48, 0x48), (0xFF49, 0x8A), (0xFF4A, 0x48), (0xFF4B, 0x98), (0xFF4C, 0x48),
                (0xFF4D, 0xBA), (0xFF4E, 0xBD), (0xFF4F, 0x04), (0xFF50, 0x01), (0xFF51, 0x29),
                (0xFF52, 0x10), (0xFF53, 0xF0), (0xFF54, 0x03), (0xFF55, 0x6C), (0xFF56, 0x16),
                (0xFF57, 0x03), (0xFF58, 0x6C), (0xFF59, 0x14), (0xFF5A, 0x03) // kernal IRQ handler
            })
            .WithCallback(0xFFD2, cpu => {
                cpu.WriteByteToMemory(0x030C, 0);
                var c = (char)cpu.ReadByteFromAccumulator();
                c = c == '\r'? '\n' : c;
                Console.Write($"{c}");
                var low = cpu.PopFromStack();
                var high = cpu.PopFromStack();
                var address = (high << 8 | low) + 1;

                cpu.SetValueToProgramCounter(address);
            })
            .WithCallback(0xE16F, cpu => {
                var low = cpu.ReadByteFromMemory(0xBB);
                var high = cpu.ReadByteFromMemory(0xBC);
                var length = cpu.ReadByteFromMemory(0xB7);
                var address = high << 8 | low;

                var filename = string.Empty;
                for (var index = 0; index < length; index++)
                {
                    filename += (char)cpu.ReadByteFromMemory(address + index);
                }

                var path = Path.Combine(directory, filename);
                var bin = File.ReadAllBytes(path);

                low = cpu.PopFromStack();
                high = cpu.PopFromStack();

                address = high << 8 | low;
                for (var index = 0; index < bin.Length; index++)
                {
                    cpu.WriteByteToMemory(address + index, bin[index]);
                }

                cpu.SetValueToProgramCounter(0x0816);
            })
            .WithCallback(0xFFE4, cpu => {
                cpu.SetValueToAccumulator(0x03);
                var low = cpu.PopFromStack();
                var high = cpu.PopFromStack();
                var address = (high << 8 | low) + 1;
                cpu.SetValueToProgramCounter(address);
            })
            .WithCallback(0x8000, cpu => {
                cpu.Stop();
            })
            .WithCallback(0xA474, cpu => {
                cpu.Stop();
            })
            .Build();

        cpu.Run();

        Console.WriteLine();
        return 0;
    }
}