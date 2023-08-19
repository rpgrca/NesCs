using NesCs.Logic.Cpu.Addressings;
using NesCs.Logic.Cpu.Operations;

namespace NesCs.Logic.Cpu.Instructions;

public class Instruction : IInstruction
{
    protected readonly IAddressing _addressing;
    protected readonly IOperation _operation;

    public byte Opcode { get; private set; }

    public string Name { get; private set; }

    public byte[] PeekOperands(Cpu6502 cpu) => _addressing.PeekOperands(cpu);

    public Instruction(byte opcode, string name, IAddressing addressing, IOperation operation)
    {
        Opcode = opcode;
        Name = name;
        _addressing = addressing;
        _operation = operation;
    }

    public virtual void Execute(Cpu6502 cpu)
    {
        var (address, value) = _addressing.ObtainValueAndAddress(cpu);
        _operation.Execute(cpu, value, address);
    }
}