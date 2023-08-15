namespace NesCs.Logic.Cpu.Instructions;

public class IllegalLaxAbsoluteOpcodeAF : IInstruction
{
    public string Name => "LAX";

    public byte Opcode => 0xAF;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        var value = cpu.ReadByteFromMemory(high << 8 | low);
        cpu.ReadyForNextInstruction();

        cpu.SetValueToAccumulator(value);
        cpu.SetValueToRegisterX(value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}