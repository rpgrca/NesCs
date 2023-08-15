namespace NesCs.Logic.Cpu.Instructions;

public class TransferAccumulatorToXOpcodeAA : TransferInstruction
{
    public override byte Opcode => 0xAA;

    public override string Name => "TAX";

    protected override byte ObtainValueFromSource(Cpu6502 cpu) =>
        cpu.ReadByteFromAccumulator();

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueToRegisterX(value);
}