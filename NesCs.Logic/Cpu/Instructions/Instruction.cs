using NesCs.Logic.Cpu.Addressings;
using NesCs.Logic.Cpu.Operations;

namespace NesCs.Logic.Cpu.Instructions;

public class Instruction : IInstruction
{
    protected readonly byte _opcode;
    protected readonly string _name;
    protected readonly IAddressing _addressing;
    protected readonly IOperation _operation;

    public Instruction(byte opcode, string name, IAddressing addressing, IOperation operation)
    {
        _opcode = opcode;
        _name = name;
        _addressing = addressing;
        _operation = operation;
    }

    public virtual void Execute(Cpu6502 cpu)
    {
        var (address, value) = _addressing.ObtainValueAndAddress(cpu);
        _operation.Execute(cpu, value, address);
    }
}