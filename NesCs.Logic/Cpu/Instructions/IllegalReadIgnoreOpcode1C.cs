namespace NesCs.Logic.Cpu.Instructions;

public class IllegalReadIgnoreOpcode1C : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        int address = 0;

#if NESDEV
        // TODO: Breaks Tom's tests
        var address = high << 8 | low + cpu.ReadByteFromRegisterX();
        _ = cpu.ReadByteFromMemory(address);

        if (low + cpu.ReadByteFromRegisterX() > 255)
        {
#endif
            address = high << 8 | ((low + cpu.ReadByteFromRegisterX()) & 255);
            _ = cpu.ReadByteFromMemory(address);
#if NESDEV
        }
#endif

        cpu.ReadyForNextInstruction();
    }
}