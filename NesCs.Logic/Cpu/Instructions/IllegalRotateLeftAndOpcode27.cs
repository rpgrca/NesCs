namespace NesCs.Logic.Cpu.Instructions;

public class IllegalRotateLeftAndOpcode27 : IInstruction
{
    public string Name => "RLA";

    public byte Opcode => 0x27;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        var value = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        cpu.WriteByteToMemory(address, value);

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
        cpu.WriteByteToMemory(address, result);

        result &= cpu.ReadByteFromAccumulator();
        cpu.SetValueToAccumulator(result);
        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }
}