using static NesCs.Logic.Cpu.Cpu6502;

namespace NesCs.Logic.Cpu.Instructions;

public class CompareAbsoluteYModeOpcodeD9 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var address = high << 8 | low + cpu.ReadByteFromRegisterY() & 0xff;
        var value = cpu.ReadByteFromMemory(address);

        var address2 = (high << 8 | low) + cpu.ReadByteFromRegisterY() & 0xffff;
        if (address != address2)
        {
            value = cpu.ReadByteFromMemory(address2);
        }

        var result = (ProcessorStatus)(cpu.ReadByteFromAccumulator() - value);
        cpu.ClearCarryFlag();
        cpu.ClearNegativeFlag();
        cpu.ClearZeroFlag();
        if (cpu.ReadByteFromAccumulator() >= value)
        {
            cpu.SetCarryFlag();
            if (cpu.ReadByteFromAccumulator() == value)
            {
                cpu.SetZeroFlag();
            }
        }

        if ((result & ProcessorStatus.N) == ProcessorStatus.N)
        {
            cpu.SetNegativeFlag();
        }
    }
}