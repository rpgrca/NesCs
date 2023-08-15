namespace NesCs.Logic.Cpu.Instructions;

public class IllegalSaxIndirectYOpcode97 : IInstruction
{
    public string Name => "SAX";

    public byte Opcode => 0x97;

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

        var value = (byte)(cpu.ReadByteFromAccumulator() & cpu.ReadByteFromRegisterX());
        cpu.WriteByteToMemory(address, value);
    }
}

