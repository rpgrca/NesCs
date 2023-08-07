namespace NesCs.Logic.Cpu.Operations;

public class ClearFlag : IOperation
{
    void IOperation.Execute(Cpu6502 cpu, byte value, int address)
    {
        var flags = cpu.GetFlags();
        flags &= ~(ProcessorStatus)value;
        cpu.OverwriteFlags(flags);
    }
}