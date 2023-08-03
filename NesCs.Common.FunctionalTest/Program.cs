public class Program
{
    public static void Main(string[] args)
    {
        //var bin = File.ReadAllBytes("../../6502_65C02_functional_tests/bin_files/6502_functional_test.bin");
        var bin = File.ReadAllBytes("../../as65/6502_functional_test.bin");
        var cpu = new NesCs.Logic.Cpu.Cpu6502.Builder()
            .Running(bin)
            .ImageStartsAt(0x0a)
            .WithSizeOf(65526)
            .WithProcessorStatusAs(NesCs.Logic.Cpu.Cpu6502.ProcessorStatus.X)
            .WithProgramCounterAs(0x400)
            .Build();

        cpu.Run();
    }
}