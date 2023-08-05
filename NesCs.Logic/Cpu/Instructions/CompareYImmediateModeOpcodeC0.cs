using static NesCs.Logic.Cpu.Cpu6502;

namespace NesCs.Logic.Cpu.Instructions;

public class CompareYImmediateModeOpcodeC0 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var value = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var result = (ProcessorStatus)(cpu.ReadByteFromRegisterY() - value);
        cpu.ClearCarryFlag();
        cpu.ClearNegativeFlag();
        cpu.ClearZeroFlag();
        if (cpu.ReadByteFromRegisterY() >= value)
        {
            cpu.SetCarryFlag();
            if (cpu.ReadByteFromRegisterY() == value)
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