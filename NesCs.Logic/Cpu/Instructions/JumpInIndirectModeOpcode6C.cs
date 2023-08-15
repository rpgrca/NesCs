namespace NesCs.Logic.Cpu.Instructions;

public class JumpInIndirectModeOpcode6C : IInstruction
{
    public string Name => "JMP";

    public byte Opcode => 0x6C;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low;
 
        var newLow = cpu.ReadByteFromMemory(address);
        var newHigh = low == 0xff ? cpu.ReadByteFromMemory(high << 8) : (int)cpu.ReadByteFromMemory(address + 1);

        address = newHigh << 8 | newLow;
        cpu.SetValueToProgramCounter(address);
    }
}