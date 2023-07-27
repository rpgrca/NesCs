namespace NesCs.Logic.Cpu;

public partial class Cpu6502
{
    [Flags]
    public enum ProcessorStatus
    {
        C = 1 << 0,
        Z = 1 << 1,
        I = 1 << 2,
        D = 1 << 3,
        B = 1 << 4,
        X = 1 << 5,
        V = 1 << 6,
        N = 1 << 7
    }
}