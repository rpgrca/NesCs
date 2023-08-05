using static NesCs.Logic.Cpu.Cpu6502;

namespace NesCs.Logic.Cpu.Instructions;

public class CompareXZeroPageModeOpcodeE4 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromMemory(address);

        var result = (ProcessorStatus)(cpu.ReadByteFromRegisterX() - value);
        cpu.ClearCarryFlag();
        cpu.ClearNegativeFlag();
        cpu.ClearZeroFlag();
        if (cpu.ReadByteFromRegisterX() >= value)
        {
            cpu.SetCarryFlag();
            if (cpu.ReadByteFromRegisterX() == value)
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