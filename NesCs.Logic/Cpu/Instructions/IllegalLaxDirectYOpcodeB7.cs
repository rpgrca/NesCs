namespace NesCs.Logic.Cpu.Instructions;

public class IllegalLaxDirectYOpcodeB7 : IInstruction
{
    public string Name => "LAX";

    public byte Opcode => 0xB7;

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1) };
        return operands;
    }

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        address = (byte)(address + cpu.ReadByteFromRegisterY());
        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromMemory(address);

        cpu.SetValueToAccumulator(value);
        cpu.SetValueToRegisterX(value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}