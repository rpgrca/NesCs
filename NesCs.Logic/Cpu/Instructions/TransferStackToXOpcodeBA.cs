namespace NesCs.Logic.Cpu.Instructions;

// https://ryukojiro.github.io/v6502/isa.html#isa_tsx got it wrong, it's transfer stack pointer, not status flags
public class TransferStackToXOpcodeBA : TransferInstruction
{
    protected override byte ObtainValueFromSource(Cpu6502 cpu) =>
        (byte)cpu.ReadByteFromStackPointer();

    protected override void StoreValueInFinalDestination(Cpu6502 cpu, byte value) =>
        cpu.SetValueIntoRegisterX(value);
}