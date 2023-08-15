namespace NesCs.Logic.Cpu.Instructions;

public class IllegalLaxIndirectYOpcodeA3 : IInstruction
{
    public string Name => "LAX";

    public byte Opcode => 0xA3;

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1) };
        return operands;
    }

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

        cpu.SetValueToAccumulator(value);
        cpu.SetValueToRegisterX(value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}