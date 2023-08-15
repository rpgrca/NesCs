namespace NesCs.Logic.Cpu.Instructions;

public class JumpInAbsoluteModeOpcode4C : IInstruction
{
    public string Name => "JMP";

    public byte Opcode => 0x4C;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low;

        cpu.SetValueToProgramCounter(address);
    }
}