namespace NesCs.Logic.Cpu.Addressings;

internal interface IAddressing : IDebuggerDisplay
{
    (int Address, byte Value) ObtainValueAndAddress(Cpu6502 cpu);
    byte[] PeekOperands(Cpu6502 cpu);
}