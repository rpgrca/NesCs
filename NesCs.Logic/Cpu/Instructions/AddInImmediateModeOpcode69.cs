using NesCs.Logic.Cpu.Addressings;
using NesCs.Logic.Cpu.Operations;

namespace NesCs.Logic.Cpu.Instructions;

public class AddInImmediateModeOpcode69 : IInstruction
{
    private readonly IAddressing _addressing;
    private readonly IOperation _operation;

    public AddInImmediateModeOpcode69(IAddressing addressing, IOperation operation)
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