namespace NesCs.Logic.Cpu.Operations;

internal interface IFlagFactory
{
    IOperation D { get; }
    IOperation V { get; }
    IOperation I { get; }
    IOperation C { get; }
}