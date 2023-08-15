using NesCs.Logic.Cpu.Addressings;
using NesCs.Logic.Cpu.Operations;

namespace NesCs.Logic.Cpu.Instructions;

public class IllegalInstruction : IInstruction
{
    protected readonly IAddressing _addressing;
    protected readonly IOperation _first;
    protected readonly IOperation _second;

    public byte Opcode { get; private set; }
    public string Name { get; private set; }

    public byte[] PeekOperands(Cpu6502 cpu) => _addressing.PeekOperands(cpu);

    public IllegalInstruction(byte opcode, string name, IAddressing addressing, IOperation first, IOperation second)
    {
        Opcode = opcode;
        Name = name;
        _addressing = addressing;
        _first = first;
        _second = second;
    }

    public void Execute(Cpu6502 cpu)
    {
        var (address, value) = _addressing.ObtainValueAndAddress(cpu);
        (address, value) = _first.Execute(cpu, value, address);
        _second.Execute(cpu, value, address);
    }
}