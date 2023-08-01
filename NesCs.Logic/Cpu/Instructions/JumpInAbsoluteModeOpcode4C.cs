namespace NesCs.Logic.Cpu.Instructions;

public class JumpInAbsoluteModeOpcode4C : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low;

        cpu.SetValueIntoProgramCounter(address);
    }
}