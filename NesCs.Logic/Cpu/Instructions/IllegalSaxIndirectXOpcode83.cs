namespace NesCs.Logic.Cpu.Instructions;

public class IllegalSaxIndirectXOpcode83 : IInstruction
{
    public string Name => "SAX";

    public byte Opcode => 0x83;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        address = (byte)(address + cpu.ReadByteFromRegisterX());
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromMemory(address);

        address = (byte)(address + 1);
        var high = cpu.ReadByteFromMemory(address);

        var effectiveAddress = high << 8 | low;
        var value = (byte)(cpu.ReadByteFromAccumulator() & cpu.ReadByteFromRegisterX());
        cpu.WriteByteToMemory(effectiveAddress, value);
    }
}