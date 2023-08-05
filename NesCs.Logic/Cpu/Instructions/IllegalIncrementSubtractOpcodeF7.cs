namespace NesCs.Logic.Cpu.Instructions;

public class IllegalIncrementSubtractOpcodeF7 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        address = (byte)(address + cpu.ReadByteFromRegisterX());
        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromMemory(address);
        cpu.WriteByteToMemory(address, value);

        value += 1;
        cpu.WriteByteToMemory(address, value);

        var a = cpu.ReadByteFromAccumulator();

        value = (byte)~value;
        var sum = a + value + (cpu.IsReadCarryFlagSet()? 1 : 0);
        var result = (byte)(sum & 0xff);

        cpu.SetValueToAccumulator(result);
        cpu.ClearCarryFlag();
        cpu.ClearNegativeFlag();
        cpu.ClearOverflowFlag();
        cpu.ClearZeroFlag();

        // TODO: Not a real 8-bit implementation but works for the time being
        if ((sum >> 8) != 0)
        {
            cpu.SetCarryFlag();
        }

        if (((a ^ result) & (value ^ result) & 0x80) != 0)
        {
            cpu.SetOverflowFlag();
        }

        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }
}