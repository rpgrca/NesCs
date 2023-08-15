namespace NesCs.Logic.Cpu.Instructions;

public class TransferAccumulatorToYOpcodeA8 : TransferInstruction
{
    public override byte Opcode => 0xA8;

    public override string Name => "TAY";

    protected override byte ObtainValueFromSource(Cpu6502 cpu) =>
        cpu.ReadByteFromAccumulator();

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueToRegisterY(value);
}