namespace NesCs.Logic.Cpu.Instructions;

public class OraInIndirectYModeOpcode11 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromMemory(address);
        address = (byte)((address + 1) & 0xff);
        var high = cpu.ReadByteFromMemory(address);

        var effectiveAddress = (high << 8) | ((low + cpu.ReadByteFromRegisterY()) & 0xff);
        var value = cpu.ReadByteFromMemory(effectiveAddress);

        var effectiveAddress2 = (((high << 8) | low) + cpu.ReadByteFromRegisterY()) & 0xffff;
        if (effectiveAddress != effectiveAddress2)
        {
            value = cpu.ReadByteFromMemory(effectiveAddress2);
        }

        value |= cpu.ReadByteFromAccumulator();
        cpu.SetValueIntoAccumulator(value);

        cpu.SetZeroFlagBasedOnAccumulator();
        cpu.SetNegativeFlagBasedOnAccumulator();
    }
}