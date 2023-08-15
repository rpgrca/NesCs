namespace NesCs.Logic.Cpu.Instructions;

public class IllegalRotateLeftAndOpcode23 : IInstruction
{
    public string Name => "RLA";

    public byte Opcode => 0x23;

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
        var value = cpu.ReadByteFromMemory(effectiveAddress);
        cpu.WriteByteToMemory(effectiveAddress, value);

        int rotatedValue = (value << 1) | (cpu.IsReadCarryFlagSet()? 1 : 0);
        if ((rotatedValue >> 8) != 0)
        {
            cpu.SetCarryFlag();
        }
        else
        {
            cpu.ClearCarryFlag();
        }

        var result = (byte)(rotatedValue & 0xff);
        cpu.WriteByteToMemory(effectiveAddress, result);

        result &= cpu.ReadByteFromAccumulator();
        cpu.SetValueToAccumulator(result);
        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }
}