namespace NesCs.Logic.Cpu.Instructions;

public class TransferYToAccumulatorOpcode98 : TransferInstruction
{
    protected override byte ObtainValueFromSource(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterY();

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoAccumulator(value);
}