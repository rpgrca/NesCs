namespace NesCs.Logic.Cpu.Instructions;

public class NotImplementedInstruction : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        throw new NotImplementedException();
    }
}