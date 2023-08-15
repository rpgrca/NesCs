namespace NesCs.Logic.Cpu.Instructions;

public class TransferYToAccumulatorOpcode98 : TransferInstruction
{
    public override byte Opcode => 0x98;

    public override string Name => "TYA";

    protected override byte ObtainValueFromSource(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterY();

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueToAccumulator(value);
}