using static NesCs.Logic.Cpu.Cpu6502;

namespace NesCs.Logic.Cpu.Instructions;

public class CompareIndirectXModeOpcodeC1 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        _ = cpu.ReadByteFromMemory(address);

        address = (byte)(address + cpu.ReadByteFromRegisterX());
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromMemory(address);

        address = (byte)(address + 1);
        var high = cpu.ReadByteFromMemory(address);

        var effectiveAddress = high << 8 | low;
        var value = cpu.ReadByteFromMemory(effectiveAddress);

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