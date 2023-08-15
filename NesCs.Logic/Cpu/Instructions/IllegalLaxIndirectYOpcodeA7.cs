namespace NesCs.Logic.Cpu.Instructions;

public class IllegalLaxIndirectOpcodeA7 : IInstruction
{
    public string Name => "LAX";

    public byte Opcode => 0xA7;

    public void Execute(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var address = cpu.ReadByteFromProgram();
        var value = cpu.ReadByteFromMemory(address);

        cpu.ReadyForNextInstruction();

        cpu.SetValueToAccumulator(value);
        cpu.SetValueToRegisterX(value);

        cpu.SetZeroFlagBasedOn(value);
        cpu.SetNegativeFlagBasedOn(value);
    }
}