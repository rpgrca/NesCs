namespace NesCs.Logic.Cpu.Instructions;

public class StoreAccumulatorAbsoluteYOpcode99 : IInstruction
{
    public string Name => "STA";

    public byte Opcode => 0x99;

    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1), cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 2) };
        return operands;
    }

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low + cpu.ReadByteFromRegisterY() & 0xff;
        _ = cpu.ReadByteFromMemory(address);

        var address2 = (high << 8 | low) + cpu.ReadByteFromRegisterY() & 0xffff;
        if (address != address2)
        {
            address = address2;
        }

        cpu.WriteByteToMemory(address, cpu.ReadByteFromAccumulator());
    }
}