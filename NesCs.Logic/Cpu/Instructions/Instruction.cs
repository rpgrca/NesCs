using NesCs.Logic.Cpu.Addressings;
using NesCs.Logic.Cpu.Operations;

namespace NesCs.Logic.Cpu.Instructions;

public class Instruction : IInstruction
{
    public readonly byte _opcode;
    private readonly string _name;
    private readonly IAddressing _addressing;
    private readonly IOperation _operation;

    public Instruction(byte opcode, string name, IAddressing addressing, IOperation operation)
    {
        _opcode = opcode;
        _name = name;
        _addressing = addressing;
        _operation = operation;
    }

    void IInstruction.Execute(Cpu6502 cpu)
    {
        var value = _addressing.ObtainValue(cpu);
        _operation.Execute(cpu, value);
    }
}