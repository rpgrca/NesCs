using NesCs.Logic.Cpu.Operations;
using NesCs.Logic.Cpu.Addressings;

namespace NesCs.Logic.Cpu.Instructions;

public class AddInAbsoluteModeOpcode6D : IInstruction
{
    private readonly IAddressing _addressing;
    private readonly IOperation _operation;

    public AddInAbsoluteModeOpcode6D(IAddressing addressing, IOperation operation)
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