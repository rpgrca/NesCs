namespace NesCs.Logic.Cpu.Operations;

public interface IFlagOperation
{
    IFlagFactory Minus { get; }
    IFlagFactory Plus { get; }
}