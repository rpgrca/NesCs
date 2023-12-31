namespace NesCs.Logic.Cpu;

[Flags]
public enum ProcessorStatus
{
    None = 0,
    C = 1,
    Z = 2,
    I = 4,
    D = 8,
    B = 16,
    X = 32,
    V = 64,
    N = 128,
}