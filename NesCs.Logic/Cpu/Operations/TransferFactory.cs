namespace NesCs.Logic.Cpu.Operations;

public class TransferFactory : ITransferFactory
{
    public IOperation Accumulator { get; } = new Transfer((c, _, v) => c.SetValueToAccumulator(v));

    public IOperation X { get; } = new Transfer((c, _, v) => c.SetValueToRegisterX(v));

    public IOperation Y { get; } = new Transfer((c, _, v) => c.SetValueToRegisterY(v));
}