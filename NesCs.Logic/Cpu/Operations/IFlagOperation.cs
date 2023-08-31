namespace NesCs.Logic.Cpu.Operations;

internal interface IFlagOperation
{
    IFlagFactory Minus { get; }
    IFlagFactory Plus { get; }
}