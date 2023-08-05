namespace NesCs.Logic.Cpu.Instructions;

public class TransferXToAccumulatorOpcode8A : TransferInstruction
{
    protected override byte ObtainValueFromSource(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterX();

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueToAccumulator(value);
}