namespace NesCs.Logic.Cpu.Instructions;

public class IllegalSaxAbsoluteOpcode8F : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low;
        
        var value = (byte)(cpu.ReadByteFromAccumulator() & cpu.ReadByteFromRegisterX());
        cpu.WriteByteToMemory(address, value);
    }
}