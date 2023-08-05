namespace NesCs.Logic.Cpu.Instructions;

public class IllegalLaxIndirectYOpcodeB3 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        var low = cpu.ReadByteFromMemory(address);

        address = (byte)(address + 1 & 0xff);
        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromMemory(address);

        var effectiveAddress = high << 8 | low + cpu.ReadByteFromRegisterY() & 0xff;
        var value = cpu.ReadByteFromMemory(effectiveAddress);

        var effectiveAddress2 = (high << 8 | low) + cpu.ReadByteFromRegisterY() & 0xffff;
        if (effectiveAddress != effectiveAddress2)
        {
            value = cpu.ReadByteFromMemory(effectiveAddress2);
        }

        cpu.SetValueToAccumulator(value);
        cpu.SetValueToRegisterX(value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}