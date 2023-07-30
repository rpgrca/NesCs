using static NesCs.Logic.Cpu.Cpu6502;

namespace NesCs.Logic.Cpu.Instructions;

public class SubtractInImmediateModeOpcodeE9 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var a = cpu.ReadByteFromAccumulator();



























        value = (byte)~value;
        var sum = a + value + (cpu.ReadCarryFlag() == ProcessorStatus.C? 1 : 0);
        var result = (byte)(sum & 0xff);

        cpu.SetValueIntoAccumulator(result);
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