namespace NesCs.Logic.Cpu.Instructions;

public class IncrementMemoryZeroPageOpcodeE6 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromMemory(cpu.ReadByteFromProgramCounter());

        var value = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();
        cpu.WriteByteToMemory(address, value);
        value++;
        cpu.WriteByteToMemory(address, value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}