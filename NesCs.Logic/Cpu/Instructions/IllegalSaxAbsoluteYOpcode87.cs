namespace NesCs.Logic.Cpu.Instructions;

public class IllegalSaxAbsoluteYOpcode87 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var value = (byte)(cpu.ReadByteFromAccumulator() & cpu.ReadByteFromRegisterX());
        cpu.WriteByteToMemory(address, value);
    }
}