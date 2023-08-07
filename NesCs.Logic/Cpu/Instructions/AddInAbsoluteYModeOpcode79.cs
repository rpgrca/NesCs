using NesCs.Logic.Cpu.Addressings;
using NesCs.Logic.Cpu.Operations;

namespace NesCs.Logic.Cpu.Instructions;

public class AddInAbsoluteYModeOpcode79 : IInstruction
{
    private readonly IAddressing _addressing;
    private readonly IOperation _operation;

    public AddInAbsoluteYModeOpcode79(IAddressing addressing, IOperation operation)
    {
        _addressing = addressing;
        _operation = operation;
    }

    public void Execute(Cpu6502 cpu)
    {
        var value = _addressing.ObtainValue(cpu);
        _operation.Execute(cpu, value);
    }
}