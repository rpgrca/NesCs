using NesCs.Logic.Cpu.Addressings;
using NesCs.Logic.Cpu.Operations;

namespace NesCs.Logic.Cpu.Instructions;

public class ClearFlagInstruction : Instruction
{
    private readonly ProcessorStatus _flag;

    public ClearFlagInstruction(byte opcode, string name, IAddressing addressing, IOperation operation, ProcessorStatus flag)
        : base(opcode, name, addressing, operation)
    {
        _flag = flag;
    }

    public override void Execute(Cpu6502 cpu)
    {
        _ = _addressing.ObtainValueAndAddress(cpu);
        _operation.Execute(cpu, (byte)_flag, 0);
    }
}