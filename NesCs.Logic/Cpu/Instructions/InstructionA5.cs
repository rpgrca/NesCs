namespace NesCs.Logic.Cpu.Instructions;

public class InstructionA5 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var a = cpu.ReadByteFromMemory(address);
        cpu.SetValueIntoAccumulator(a);

        cpu.SetZeroFlagBasedOnAccumulator();
        cpu.SetNegativeFlagBasedOnAccumulator();
    }
}