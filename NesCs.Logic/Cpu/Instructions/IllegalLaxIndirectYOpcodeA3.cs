namespace NesCs.Logic.Cpu.Instructions;

public class IllegalLaxIndirectYOpcodeA3 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address1 = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address1);

        var address = (address1 + cpu.ReadByteFromRegisterX()) & 0xff;
        cpu.ReadyForNextInstruction();

        var low = cpu.ReadByteFromMemory(address);
        var high = cpu.ReadByteFromMemory((address + 1) & 0xff);

        address = (high << 8) | low;
        var value = cpu.ReadByteFromMemory(address);

        cpu.SetValueIntoAccumulator(value);
        cpu.SetValueIntoRegisterX(value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}