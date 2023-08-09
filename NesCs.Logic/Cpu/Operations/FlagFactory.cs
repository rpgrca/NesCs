namespace NesCs.Logic.Cpu.Operations;

public class FlagFactory : IFlagFactory
{
    public IOperation D { get; }
    public IOperation V { get; }
    public IOperation I { get; }
    public IOperation C { get; }

    public FlagFactory(Action<Cpu6502> onD, Action<Cpu6502> onV, Action<Cpu6502> onI, Action<Cpu6502> onC)
    {
        D = new Flag(onD);
        V = new Flag(onV);
        I = new Flag(onI);
        C = new Flag(onC);
    }
}