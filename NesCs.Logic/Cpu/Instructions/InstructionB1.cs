namespace NesCs.Logic.Cpu.Instructions;

public class InstructionB1 : IInstruction
{
    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromMemory(address);
        address = (byte)((address + 1) & 0xff);
        var high = cpu.ReadByteFromMemory(address);

        var effectiveAddress = (high << 8) | ((low + cpu.ReadByteFromRegisterY()) & 0xff);
        var a = cpu.ReadByteFromMemory(effectiveAddress);
        cpu.SetValueIntoAccumulator(a);

        var effectiveAddress2 = (((high << 8) | low) + cpu.ReadByteFromRegisterY()) & 0xffff;
        if (effectiveAddress != effectiveAddress2)
        {
            a = cpu.ReadByteFromMemory(effectiveAddress2);
            cpu.SetValueIntoAccumulator(a);
        }

        cpu.SetZeroFlagBasedOnAccumulator();
        cpu.SetNegativeFlagBasedOnAccumulator();
    }
}