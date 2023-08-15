namespace NesCs.Logic.Cpu.Instructions;

public class IllegalSaxAbsoluteYOpcode87 : IInstruction
{
    public string Name => "SAX";

    public byte Opcode => 0x87;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var value = (byte)(cpu.ReadByteFromAccumulator() & cpu.ReadByteFromRegisterX());
        cpu.WriteByteToMemory(address, value);
    }
}