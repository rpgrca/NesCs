namespace NesCs.Logic.Cpu.Operations;

public class TransferFactory : ITransferFactory
{
    public IOperation Accumulator { get; } = new Transfer((c, _, v) => c.SetValueToAccumulator(v));
}