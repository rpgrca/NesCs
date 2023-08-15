namespace NesCs.Logic.Cpu.Instructions;

public class TransferXToAccumulatorOpcode8A : TransferInstruction
{
    public override byte Opcode => 0x8A;

    public override string Name => "TXA";

    protected override byte ObtainValueFromSource(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterX();

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueToAccumulator(value);
}