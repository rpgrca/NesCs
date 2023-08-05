namespace NesCs.Logic.Cpu.Instructions;

public class TransferAccumulatorToXOpcodeAA : TransferInstruction
{
    protected override byte ObtainValueFromSource(Cpu6502 cpu) =>
        cpu.ReadByteFromAccumulator();

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueToRegisterX(value);
}